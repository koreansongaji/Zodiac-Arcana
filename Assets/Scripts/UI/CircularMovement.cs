using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMovement : MonoBehaviour
{
    [SerializeField] private float _minorAxis;
    [SerializeField] private float _majorAxis;
    [SerializeField] private float speed;
    private RectTransform _transform;
    private Vector2 _center;

    private void Start()
    {
        _transform = GetComponent<RectTransform>();
        _center = _transform.anchoredPosition;
    }

    private void Update()
    {
        float angle = Time.time * speed;
        float x = Mathf.Cos(angle) * _majorAxis;
        float y = Mathf.Sin(angle) * _minorAxis;

        _transform.anchoredPosition = _center + new Vector2(x, y);

    }

}
