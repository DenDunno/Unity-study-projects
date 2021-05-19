using UnityEngine;


class SpaceShipMovement : MonoBehaviour
{       
    private readonly float _sideSpeed = 5f;
    private readonly float _maxSideOffset = 2.7f;

    private readonly float _rotationSpeed = 3f;
    private readonly int _rotationAngle = 40;


    private void Update()
    {
        int sgnX = (int)Input.GetAxisRaw("Horizontal");

        Move(Vector3.right * sgnX);
        Rotate(Quaternion.Euler(0, 0, _rotationAngle * sgnX));
    }


    private void Move(Vector3 direction)
    {
        var newPosition = Vector3.MoveTowards(transform.position, transform.position + direction, _sideSpeed * Time.deltaTime);

        if (Mathf.Abs(newPosition.x) <= _maxSideOffset)
        {
            transform.position = newPosition;
        }
    }


    private void Rotate(Quaternion rotation)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
    }
}