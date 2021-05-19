using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class SpaceShipAcceleration : MonoBehaviour
{
    public static float Speed { get; private set; } = 4;
    public float DistancePassed { get; private set; } = 0f;

    private readonly float _minSpeed = 4;
    
    private List<ParticleSystem> _accelerationEffects;
    private bool _isAcceleration = false;
    private float _acceleration = 2f;

    [Inject]
    private void Construct(ParticleSystem[] effects)
    {
        _accelerationEffects = effects.ToList();
    }


    private void Update()
    {
        Speed += _acceleration * Time.deltaTime;
        DistancePassed += Speed * Time.deltaTime;

        int sgn = (int)Input.GetAxisRaw("Vertical");

        ManageSpeed((SpeedCommand)sgn);
    }
    

    private void ManageSpeed(SpeedCommand speedCommand)
    {
        switch (speedCommand)
        {
            case SpeedCommand.SlowDown:
                SlowDown(-25);
            break;

            case SpeedCommand.SpeedUp:
                SpeedUp(10);
            break;

            default:
                _accelerationEffects.ForEach(effect => effect.Stop());
                _isAcceleration = false;
            break;
        }
    }


    private void SpeedUp(float acceleration)
    {
        if (_isAcceleration == false)
        {
            _accelerationEffects.ForEach(effect => effect.Play());
            _isAcceleration = true;
        }

        Speed += acceleration * Time.deltaTime;
    }


    private void SlowDown(float acceleration)
    {
        if (Speed >= _minSpeed)
        {
            Speed += acceleration * Time.deltaTime;
        }
    }


    public void Stop()
    {
        Speed = 0;
        _acceleration = 0;
        _accelerationEffects.ForEach(effect => effect.Stop());
    }
}
