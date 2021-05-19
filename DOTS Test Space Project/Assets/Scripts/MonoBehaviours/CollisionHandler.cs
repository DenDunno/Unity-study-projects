using System;
using Unity.Entities;
using UnityEngine;
using Unity.Collections;
using Cysharp.Threading.Tasks;

class CollisionHandler : MonoBehaviour
{
    [SerializeField] private UIManager _UIManager;
    private EntityManager _entityManager;

    private CollisionSystem _collisionSystem;
    private ParticleSystem _exposion;


    private void Start()
    {
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        _collisionSystem = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<CollisionSystem>();
        _collisionSystem.SpaceShipCollided += OnSpaceShipCollided;

        _exposion = Resources.Load<ParticleSystem>("Prefabs/Explosion");
    }


    private async void OnSpaceShipCollided(object sender , EventArgs e)
    {
        StopRoad();
        ExplodeSpaceShip();
        await WaitAndShowPanel();
    }


    private void ExplodeSpaceShip()
    {
        var spaceShipEntity = _entityManager.CreateEntityQuery(typeof(SpaceShipComponent)).ToEntityArray(Allocator.Persistent);
        Instantiate(_exposion, transform.position, Quaternion.identity);

        _entityManager.DestroyEntity(spaceShipEntity[0]);
        spaceShipEntity.Dispose();

        Destroy(gameObject);
    }


    private void StopRoad()
    {
        var road = _entityManager.CreateEntityQuery(typeof(RoadComponent)).ToEntityArray(Allocator.Persistent);

        _entityManager.DestroyEntity(road[0]);

        road.Dispose();
    }


    private async UniTask WaitAndShowPanel()
    {
        float waitTime = 2f;
        await UniTask.Delay(TimeSpan.FromSeconds(waitTime), ignoreTimeScale: false);
        _UIManager.ShowGameOverPanel();
    }


    private void OnDestroy()
    {
        if (_collisionSystem != null)
            _collisionSystem.SpaceShipCollided -= OnSpaceShipCollided;
    }
}