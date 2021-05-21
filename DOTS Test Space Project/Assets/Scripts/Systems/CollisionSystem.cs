using UnityEngine;
using Unity.Transforms;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Collections;
using System;
using Unity.Jobs;


public class CollisionSystem : JobComponentSystem
{
    public event EventHandler SpaceShipCollided;

    private struct CollisionEvent { }
    private NativeQueue<CollisionEvent> _eventQueue;


    protected override void OnCreate()
    {
        _eventQueue = new NativeQueue<CollisionEvent>(Allocator.Persistent);
    }


    protected override void OnDestroy()
    {
        _eventQueue.Dispose();
    }


    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var eventQueueParallel = _eventQueue.AsParallelWriter();
        var spaceShipPosition = float3.zero;

        Entities.ForEach((ref SpaceShipComponent tag, ref Translation translation) =>
        {
            spaceShipPosition = translation.Value;
        }).Run();


        JobHandle job = Entities.ForEach((ref ObstacleComponent tag, ref Translation translation) =>
        {
            if (math.distance(spaceShipPosition, translation.Value) <= 1.5f)
            {
                eventQueueParallel.Enqueue(new CollisionEvent());
            }

        }).Schedule(inputDeps);

        job.Complete();

        while(_eventQueue.TryDequeue(out CollisionEvent collisionEvent))
        {
            SpaceShipCollided?.Invoke(this , EventArgs.Empty);
        }

        return job;
    }
}
