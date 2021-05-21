using Unity.Entities;


[GenerateAuthoringComponent]
public struct SpaceShipComponent : IComponentData
{
    public float SideSpeed;
    public float MaxSideOffset;

    public float RotationSpeed;
    public float RotationAngle;
}
