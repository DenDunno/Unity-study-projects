using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[GenerateAuthoringComponent]
public class CameraFollowingComponent : IComponentData
{
    public float FollowingSpeed;
    public float RotationSpeed;

    public float3[] CameraPositions;

    public Camera Camera;
}