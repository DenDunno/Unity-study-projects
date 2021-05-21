using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using Unity.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _obstacles;

    private readonly int _numOfObstacles = 10;
    private readonly float _obstacleXOffset = 2.7f;
    private readonly float _distanceBetweenObstacles = 15f;

    private List<Entity> _obstacleEntities = new List<Entity>();
    private EntityManager _entityManager;
    private BlobAssetStore _blobAssetStore;


    private void Start()
    {
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        var numOfDefaultEntites = 10;

        if (_entityManager.GetAllEntities().Length < numOfDefaultEntites) // if obstacles don't exist
        {
            SetSettings();
            SpawnObstacles();
        }

        else
        {
            MoveExistingObstacles();
        }
    }


    private void SetSettings()
    {
        _blobAssetStore = new BlobAssetStore();
        GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, _blobAssetStore);

        foreach (var obstacle in _obstacles)
        {
            _obstacleEntities.Add(GameObjectConversionUtility.ConvertGameObjectHierarchy(obstacle, settings));
        }
    }


    private void SpawnObstacles()
    {
        for (var i = 0; i < _numOfObstacles; ++i)
        {
            Entity newObstacle = _entityManager.Instantiate(_obstacleEntities[Random.Range(0, _obstacleEntities.Count)]);
            _entityManager.SetComponentData(newObstacle, new Translation { Value = GetObstacleOffset(i) });
        }
    }


    private float3 GetObstacleOffset(int depthIndex)
    {
        var sgn = Random.Range(-1, 2);

        var x_offset = sgn * _obstacleXOffset;
        var z_offset = depthIndex * _distanceBetweenObstacles;

        return new float3(x_offset, 1, z_offset);
    }


    private void MoveExistingObstacles()
    {
        var obstacles = _entityManager.CreateEntityQuery(typeof(ObstacleComponent)).ToEntityArray(Allocator.Persistent);

        foreach (var obstacle in obstacles)
        {
            var position = _entityManager.GetComponentData<Translation>(obstacle).Value;
            position = new float3(position.x, position.y, position.z + 66);

            _entityManager.SetComponentData(obstacle, new Translation { Value = position });
        }

        obstacles.Dispose();
    }


    private void OnDestroy()
    {
        _blobAssetStore?.Dispose();
    }
}

