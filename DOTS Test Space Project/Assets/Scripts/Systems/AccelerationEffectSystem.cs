using UnityEngine;
using Unity.Transforms;
using Unity.Entities;

public class AccelerationEffectSystem : ComponentSystem
{
    private bool _isAcceleration = false;


    protected override void OnUpdate()
    {
        var sgn = (int)Input.GetAxisRaw("Vertical");

        Entities.ForEach((AccelerationEffectComponent effectComponent)=>
        {
            if (sgn == 1)
            {
                if (_isAcceleration == false)
                {
                    effectComponent.Effects.ForEach(effect => effect.Play());
                    _isAcceleration = true;
                }
            }

            else
            {
                effectComponent.Effects.ForEach(effect => effect.Stop());
                _isAcceleration = false;
            }
        });
    }
}
