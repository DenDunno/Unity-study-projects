using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Physics;

public class ObstacleSystem : ComponentSystem
{
    private readonly float _distanceBetweenObstacles = 15f;


    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;
        float roadSpeed = 0;

        Entities.ForEach((ref RoadComponent road) =>
        {
            roadSpeed = road.Speed;
        });


        Entities.ForEach((ref ObstacleComponent obstacle, ref Translation translation, ref Rotation rotation) =>
        {
            float3 offset = obstacle.RotationAngles * obstacle.RotationSpeed * deltaTime;
            rotation.Value = math.mul(rotation.Value, quaternion.Euler(offset));

            translation.Value += obstacle.Direction * roadSpeed * obstacle.MovementSpeed * deltaTime;

            if (translation.Value.z <= obstacle.RespawnEdge)
            {
                MoveObstacle(ref translation, ref obstacle);
            }
        });
    }


    private void MoveObstacle(ref Translation translation , ref ObstacleComponent obstacle)
    {
        float z_position = 0f;
        float x_position = UnityEngine.Random.Range(-1, 2) * obstacle.SideEdgeSpawn;

        Entities.ForEach((ref ObstacleComponent obstacle, ref Translation translation) =>
        {
            if (translation.Value.z > z_position)
            {
                z_position = translation.Value.z;
            }
        });

        z_position += _distanceBetweenObstacles;

        translation.Value = new float3(x_position, 1, z_position);
    }
}

