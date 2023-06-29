using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetValueZ : MonoBehaviour
{
    private Vector2 _abscissaAxis;
    private Vector2 _viewVector;
    private float scalarComposition;
    private float mudulesComposition;
    private float division;
    private float angle;
    private Camera _camera;
    private Transform _thisTransform;

    private void Start()
    {
        _camera = Camera.main;
        _abscissaAxis = Vector2.right;
        _thisTransform = transform;
    }

    public float GetValue(out Vector2 abscissaAxis, out Vector2 viewVector)
    {
        _viewVector = _camera.ScreenToWorldPoint(Input.mousePosition) - _thisTransform.position;
        scalarComposition = _abscissaAxis.x * _viewVector.x + _abscissaAxis.y * _viewVector.y;
        mudulesComposition = _abscissaAxis.magnitude * _viewVector.magnitude;
        division = scalarComposition / mudulesComposition;
        angle = Mathf.Acos(division) * Mathf.Rad2Deg;
        abscissaAxis = _abscissaAxis;
        viewVector = _viewVector;

        return angle;
    }

    public float GetValue()
    {
        _viewVector = _camera.ScreenToWorldPoint(Input.mousePosition) - _thisTransform.position;
        scalarComposition = _abscissaAxis.x * _viewVector.x + _abscissaAxis.y * _viewVector.y;
        mudulesComposition = _abscissaAxis.magnitude * _viewVector.magnitude;
        division = scalarComposition / mudulesComposition;
        angle = Mathf.Acos(division) * Mathf.Rad2Deg;

        return angle;
    }
}
