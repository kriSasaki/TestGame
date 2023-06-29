using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[RequireComponent(typeof(LookDirection))]
public class EnemyMeleeWeapon : EnemyWeapon
{
    private LookDirection _lookDirection;

    private bool IslookingUp;

    private const string State = "State";

    private void Start()
    {
        _lookDirection = GetComponent<LookDirection>();
    }

    private void Update()
    {
        IslookingUp = _lookDirection.IsLookingUp(_target.transform.position);

        if (IslookingUp)
        {
            _animator.SetInteger(State, 1);
        }
        else
        {
            _animator.SetInteger(State, 0);
        }
    }

    public override void Attack()
    {
        if (IslookingUp)
        {
            _audio.Play();
            _animator.SetInteger(State, 3);
        }
        else
        {
            _audio.Play();
            _animator.SetInteger(State, 2);
        }
    }
}
