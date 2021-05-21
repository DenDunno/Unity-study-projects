using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;


public class SpaceShipMovement : ComponentSystem
{
    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;
        int sgn = (int)Input.GetAxisRaw("Horizontal");

        Entities.ForEach((ref SpaceShipComponent spaceShip, ref Translation translation , ref Rotation rotation) =>
        {
            float3 direction = Vector3.right * sgn;
            quaternion newRotation = quaternion.Euler(0 , 0 , spaceShip.RotationAngle * sgn);

            float3 newPosition = translation.Value + direction * spaceShip.SideSpeed * deltaTime;

            if (Mathf.Abs(newPosition.x) <= spaceShip.MaxSideOffset)
            {
                translation.Value = newPosition;
            }
            
            rotation.Value = math.nlerp(rotation.Value, newRotation, spaceShip.RotationSpeed * deltaTime);
        }); 
    }
}
