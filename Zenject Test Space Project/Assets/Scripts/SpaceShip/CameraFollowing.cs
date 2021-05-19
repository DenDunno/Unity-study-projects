using System.Collections;
using System.Collections.Generic;
using UnityEngine;


internal struct CameraView
{
    public Quaternion Rotation;
    public Vector3 Offset;

    public CameraView(Quaternion rotation, Vector3 offset)
    {
        Rotation = rotation;
        Offset = offset;
    }
}


public class CameraFollowing : MonoBehaviour
{
    private readonly float _followingSpeed = 4f;
    private readonly float _rotationSpeed = 2f;
    private Transform _cameraTransform;

    private CameraView[] _cameraViews;
    private int _cameraViewIndex;


    private void Start()
    {
        _cameraTransform = Camera.main.transform;

        _cameraViews = new CameraView[3]
        {
            new CameraView(Quaternion.Euler(32.71f , 0.034f , 0) , new Vector3(-0.043f , 3.19f , -65.33f) - transform.position) ,
            new CameraView(Quaternion.Euler(32.7f , -0.14f , 0) , new Vector3(-0.04f , 3.7f , -66.12f) - transform.position)   ,
            new CameraView(Quaternion.Euler(33.38f , 0.03f , 0)  , new Vector3(-0.04f , 3.92f , -67.05f) - transform.position)  ,
        };
    }


    private void Update()
    {
        _cameraViewIndex = (int)Input.GetAxisRaw("Vertical") + 1;
    }


    private void LateUpdate()
    {
        Vector3 newPosition = transform.position + _cameraViews[_cameraViewIndex].Offset;

        _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, newPosition, _followingSpeed * Time.deltaTime);    
        _cameraTransform.rotation = Quaternion.Lerp(_cameraTransform.rotation, _cameraViews[_cameraViewIndex].Rotation, _rotationSpeed * Time.deltaTime);
    }
}
