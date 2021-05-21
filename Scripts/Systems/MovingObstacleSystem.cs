using UnityEngine;
using Unity.Transforms;
using Unity.Entities;
using Unity.Mathematics;
using Random = UnityEngine.Random;


public class MovingObstacleSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref MovingObstacleComponent obstacle, ref Translation translation) =>
        {
            if (obstacle.IsInited == false)
            {
                Init(ref obstacle);
            }

            obstacle.Time += Time.DeltaTime;

            if (obstacle.Time >= obstacle.Period)
                obstacle.Time = 0;

            obstacle.Lerp = (float)(1 - Mathf.Cos(obstacle.MaxSpeed * obstacle.Time)) / 2;

            float2 offset = math.lerp(obstacle.Start, obstacle.Target, obstacle.Lerp);

            translation.Value = new float3(offset.x, offset.y, translation.Value.z);
        });
    }


    private void Init(ref MovingObstacleComponent obstacle)
    {
        obstacle.Time = Random.Range(0, obstacle.Period);
        obstacle.IsInited = true;
    }
}
