using Unity.Entities;


[GenerateAuthoringComponent]
public struct SkyboxComponent : IComponentData
{
    public float RotationSpeed;

    public float Rotation;
}
