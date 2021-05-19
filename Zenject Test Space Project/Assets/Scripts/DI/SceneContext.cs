using UnityEngine;
using Zenject;


public class SceneContext : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ParticleSystem[]>().FromMethod(CreateEffects);
        Container.InstantiatePrefabResource("Prefabs/RoadGenerator");

        var spaceShip = Container.InstantiatePrefabResource("Prefabs/SpaceShip");

        Container.Bind<SpaceShipAcceleration>()
            .FromInstance(spaceShip.GetComponent<SpaceShipAcceleration>());
    }


    private ParticleSystem[] CreateEffects()
    {
        var effect = Resources.Load<ParticleSystem>("Prefabs/AccelerationEffect");

        var leftEffect = Instantiate(effect);
        var rightEffect = Instantiate(effect);

        leftEffect.transform.position += new Vector3(8, 0, 0);
        rightEffect.transform.position += new Vector3(-8, 0, 0);

        return new ParticleSystem[] { leftEffect , rightEffect};
    }
}