using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;


public class SpaceShipMovement : ComponentSystem
{
    protected override void OnUpdate()
    {
        var deltaTime = Time.DeltaTime;
        var sgn = (int)Input.GetAxisRaw("Horizontal");

        Entities.ForEach((ref SpaceShipComponent spaceShip, ref Translation translation , ref Rotation rotation) =>
        {
            var direction = (float3)Vector3.right * sgn;
            var newRotation = quaternion.Euler(0 , 0 , spaceShip.RotationAngle * sgn);

            var newPosition = translation.Value + direction * spaceShip.SideSpeed * deltaTime;

            if (Mathf.Abs(newPosition.x) <= spaceShip.MaxSideOffset)
            {
                translation.Value = newPosition;
            }
            
            rotation.Value = math.nlerp(rotation.Value, newRotation, spaceShip.RotationSpeed * deltaTime);
        }); 
    }
}
