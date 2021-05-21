using Unity.Entities;


[GenerateAuthoringComponent]
public struct RoadComponent : IComponentData
{
    public float MinimumSpeed;
    public float Acceleration;

    public float Speed;
}
