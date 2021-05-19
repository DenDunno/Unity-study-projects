using Unity.Entities;
using Unity.Mathematics;


[GenerateAuthoringComponent]
public struct ObstacleComponent : IComponentData
{
    public float RotationSpeed;
    public float3 RotationAngles;

    public float MovementSpeed;
    public float3 Direction;

    public float SideEdgeSpawn;
    public float RespawnEdge;
}
