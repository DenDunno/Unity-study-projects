using Unity.Entities;
using UnityEngine;


public class RoadMovementSystem : ComponentSystem
{
    protected override void OnUpdate()
    {    
        int sgn = (int)Input.GetAxisRaw("Vertical");
        
        Entities.ForEach((ref RoadComponent road , MeshRenderer renderer) => 
        {
            MoveTexture(renderer, ref road);

            if (sgn == 1)
            {
                SpeedUp(ref road);
            }

            else if(sgn == -1 && road.Speed > road.MinimumSpeed)
            {                 
                SlowDown(ref road);
            }
        });
    }


    private void MoveTexture(MeshRenderer renderer , ref RoadComponent road)
    {
        var deltaTime = Time.DeltaTime;
        road.Speed += road.Acceleration * deltaTime;

        var material = renderer.sharedMaterial;

        var offset = material.GetTextureOffset("_MainTex").y;
        offset -= road.Speed * deltaTime;

        if (offset <= -1)
        {
            offset = 0;
        }

        material.SetTextureOffset("_MainTex", new Vector2(0, offset));
    }


    private void SpeedUp(ref RoadComponent road)
    {
        road.Speed += 5 * road.Acceleration * Time.DeltaTime;
    }


    private void SlowDown(ref RoadComponent road)
    {
        road.Speed += -10 * road.Acceleration * Time.DeltaTime;
    }
}
