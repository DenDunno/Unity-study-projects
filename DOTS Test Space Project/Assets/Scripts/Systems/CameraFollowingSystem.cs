using UnityEngine;
using Unity.Transforms;
using Unity.Entities;
using Unity.Mathematics;

public class CameraFollowingSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        var sgn = (int)Input.GetAxisRaw("Vertical") + 1;

        Entities.ForEach((CameraFollowingComponent following , ref Translation translation) =>
        {
            var deltaTime = Time.DeltaTime;
            var spaceShipPosition = translation.Value;

            var currentPosition = following.Camera.transform.position;
            var newPosition = spaceShipPosition + following.CameraPositions[sgn];
            
            following.Camera.transform.position = math.lerp(currentPosition, newPosition, following.FollowingSpeed * deltaTime);
        });
    }
}
