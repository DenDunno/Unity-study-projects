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
        _blobAssetStore = new BlobAssetStore();

        GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, _blobAssetStore);

        foreach(var obstacle in _obstacles)
        {
            _obstacleEntities.Add(GameObjectConversionUtility.ConvertGameObjectHierarchy(obstacle, settings));
        }


        if (_entityManager.GetAllEntities().Length < 10) // if obstacles don't exist
        {
            SpawnObstacles();
        }

        Destroy(gameObject);
    }


    private void SpawnObstacles()
    {
        for (int i = 0; i < _numOfObstacles; ++i)
        {
            Entity newObstacle = _entityManager.Instantiate(_obstacleEntities[Random.Range(0 , _obstacleEntities.Count)]);
            _entityManager.SetComponentData(newObstacle, new Translation { Value = GetObstacleOffset(i) });
        }
    }


    private float3 GetObstacleOffset(int depthIndex)
    {
        int sgn = Random.Range(-1, 2);

        float x_offset = sgn * _obstacleXOffset;
        float z_offset = depthIndex * _distanceBetweenObstacles;

        return new float3(x_offset, 1, z_offset);
    }


    private void OnDestroy()
    {
        _blobAssetStore.Dispose();
    }
}

