using System.Collections.Generic;
using System.Linq;
using UnityEngine;


class ObstacleSpawner : MonoBehaviour
{
    private readonly int _obstaclesPerChunk = 7;
    private readonly float _obstacleXOffset = 2.7f;    


    public void FillChunkWithObstacles(Transform roadChunk)
    {
        Obstacle[] obstacles = Resources.LoadAll<Obstacle>("Prefabs/Obstacles");
        Vector3 roadChunkPosition = roadChunk.position;

        for (int i = 0; i < _obstaclesPerChunk; ++i)
        {
            Vector3 position = roadChunkPosition + GetObstacleOffset(i);

            Instantiate(obstacles[Random.Range(0 , obstacles.Length)], position, Quaternion.identity, roadChunk);
        }
    }


    public void ShuffleObstacles(Transform roadChunk)
    {
        var obstacles = roadChunk.GetComponentsInChildren<Obstacle>().ToList();
        Vector3 roadChunkPosition = roadChunk.position;

        for (int i = 0; i < obstacles.Count; ++i)
        {
            obstacles[i].transform.position = roadChunkPosition + GetObstacleOffset(i);
        }
    }


    private Vector3 GetObstacleOffset(int depthIndex)
    {
        float distanceBetweenObstacles = RoadSpawner.RoadLength / _obstaclesPerChunk;
        int sgn = Random.Range(-1, 2);

        float x_offset = sgn * _obstacleXOffset;
        float z_offset = depthIndex * distanceBetweenObstacles - RoadSpawner.RoadLength / 2;

        return new Vector3(x_offset, 1, z_offset);
    }
}