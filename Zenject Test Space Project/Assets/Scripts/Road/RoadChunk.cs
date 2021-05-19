using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoadChunk : MonoBehaviour
{
    public event Action ChunkPassed;

   
    private void Update()
    {
        Vector3 direction = transform.position - transform.forward;
        transform.position = Vector3.MoveTowards(transform.position, direction, SpaceShipAcceleration.Speed * Time.deltaTime);
    }


    private void OnBecameInvisible()
    {
        ChunkPassed.Invoke();
    }    
}
