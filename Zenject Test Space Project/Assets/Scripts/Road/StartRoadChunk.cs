using UnityEngine;


class StartRoadChunk : RoadChunk
{
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}