using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;


[GenerateAuthoringComponent]
public class AccelerationEffectComponent : IComponentData
{
    public List<ParticleSystem> Effects;
}
