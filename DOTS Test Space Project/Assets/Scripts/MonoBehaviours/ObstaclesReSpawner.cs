using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;


public class ObstaclesReSpawner : MonoBehaviour
{
    private EntityManager _entityManager;


    private void Start()
    {
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        MoveExistingObstacles();
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
}
