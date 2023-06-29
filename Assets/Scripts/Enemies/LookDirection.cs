using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LookDirection : MonoBehaviour
{
    private Vector3 _direction;
    private Vector3 _target;
    private Animator _animator;

    private const string Direction = "Direction";

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetLookDirection(Vector3 target)
    {
        DetermineDirection(target);
        ChangeAnimator();
    }

    public bool IsLookingUp(Vector3 target)
    {
        DetermineDirection(target);

        if (_direction.y > 0 && _direction.y > Math.Abs(_direction.x))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void DetermineDirection(Vector3 target)
    {
        _target = target;
        _direction = _target - transform.position;
        _direction.Normalize();
    }

    private void ChangeAnimator()
    {
        if (_direction.y > 0 && _direction.y > Math.Abs(_direction.x))
        {
            _animator.SetInteger(Direction, 1);
        }
        
        if(_direction.x > 0 && _direction.x > Math.Abs(_direction.y))
        {
            _animator.SetInteger(Direction, 3);
        }
        
        if(_direction.y < 0 && Math.Abs(_direction.y) > Math.Abs(_direction.x))
        {
            _animator.SetInteger(Direction, 0);
        }

        if(_direction.x < 0 && Math.Abs(_direction.x) > Math.Abs(_direction.y))
        {
            _animator.SetInteger(Direction, 2);
        }
    }
}
