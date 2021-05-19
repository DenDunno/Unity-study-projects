using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class RoadSpawner : MonoBehaviour
{
    public static readonly float RoadLength = 129.9f;

    private List<RoadChunk> _roadChunks = new List<RoadChunk>();
    private ObstacleSpawner _obstacleSpawner;

   
    private void Start()
    {
        _obstacleSpawner = GetComponent<ObstacleSpawner>();
        SpawnRoad();
    }


    private void SpawnRoad()
    {
        RoadChunk roadChunk = Resources.Load<RoadChunk>("Prefabs/RoadChunk");
        StartRoadChunk startChunk = Resources.Load<StartRoadChunk>("Prefabs/StartRoadChunk");

        startChunk = Instantiate(startChunk, Vector3.zero, Quaternion.identity, transform);
        int numOfRoadChunks = 3;

        for (int i = 0; i < numOfRoadChunks; ++i)
        {
            var chunk = Instantiate(roadChunk, new Vector3(0, 0, (i + 1) * RoadLength), Quaternion.identity, transform);
            chunk.ChunkPassed += OnChunkPassed;
            _obstacleSpawner.FillChunkWithObstacles(chunk.transform);

            _roadChunks.Add(chunk);
        }
    }


    private void OnDestroy()
    {
        _roadChunks.ForEach(chunk => chunk.ChunkPassed -= OnChunkPassed);
    }


    public void OnChunkPassed()
    {
        RoadChunk firstChunk = _roadChunks.First();

        _obstacleSpawner.ShuffleObstacles(firstChunk.transform);
        firstChunk.transform.position = _roadChunks.Last().transform.position + new Vector3(0, 0, RoadLength);

        List<RoadChunk> newList = _roadChunks.GetRange(1 , _roadChunks.Count - 1);
        newList.Add(firstChunk);

        _roadChunks = newList;
    }    
}
