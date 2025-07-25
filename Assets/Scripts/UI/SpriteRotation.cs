using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRotation : MonoBehaviour
{
    [SerializeField] private float _rotationAngle;
    [SerializeField] private float _speed;
    private Quaternion _left;
    private Quaternion _right;

    private void Start()
    {
        _left = Quaternion.Euler(0, 0, -_rotationAngle);
        _right = Quaternion.Euler(0, 0, _rotationAngle);
    }

    private void Update()
    {

        float t = (Mathf.Sin(Time.time * _speed) + 1f) / 2f;
        transform.rotation = Quaternion.Lerp(_left, _right, t);

    }
}
