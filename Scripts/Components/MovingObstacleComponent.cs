using Unity.Entities;
using Unity.Mathematics;


[GenerateAuthoringComponent]
public struct MovingObstacleComponent : IComponentData
{
    public bool IsInited;

    public float MaxSpeed;
    public float MaxSideOffset;

    public float Period;
    public float Time;
    public float Lerp;

    public float2 Start;
    public float2 Target;
}
