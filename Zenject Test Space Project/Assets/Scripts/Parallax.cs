using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Parallax : MonoBehaviour
{
    private readonly float _rotationSpeed = 2f;
    private readonly string _rotationProperty = "_Rotation";

    private Material _skyboxMaterial;
    private float _rotation = 0;


    private void Start()
    {
        _skyboxMaterial = GetComponent<Skybox>().material;
    }


    private void Update()
    {
        _rotation += _rotationSpeed * Time.deltaTime;

        if (_rotation >= 360)
            _rotation = 0;

        _skyboxMaterial.SetFloat(_rotationProperty, _rotation);
    }
}
