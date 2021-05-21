using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;


public class SkyboxSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref SkyboxComponent skyboxData , Skybox skybox) =>
        {
            skyboxData.Rotation += skyboxData.RotationSpeed * Time.DeltaTime;

            if (skyboxData.Rotation >= 360)
            {
                skyboxData.Rotation = 0;
            }

            if (skybox != null)
            {
                skybox.material.SetFloat("_Rotation", skyboxData.Rotation);
            }
        });
    }
}
