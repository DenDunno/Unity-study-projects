using UnityEngine;


class MovingObstacle : Obstacle
{
    private readonly float _maxSpeed = 2f;
    private readonly float _maxSideOffset = 3f;

    private float _period;
    private float _time;
    private float _lerp;

    private Vector3 _start;
    private Vector3 _target;


    private void Start()
    {
        _period = 2 * Mathf.PI / _maxSpeed;
        _time = Random.Range(0, _period);

        _start = new Vector3(_maxSideOffset, transform.localPosition.y, transform.localPosition.z);
        _target = new Vector3(-_maxSideOffset, transform.localPosition.y, transform.localPosition.z);
    }


    private void Update()
    {
        _time += Time.deltaTime;

        if (_time >= _period)
            _time = 0;

        _lerp = (float)(1 - Mathf.Cos(_maxSpeed * _time)) / 2;

        transform.localPosition = Vector3.Lerp(_start, _target, _lerp);
    }
}

