using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GetDamageState : State
{
    private Animator _animator;
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _enemy.GetDamage += PlayGetDamage;
    }

    private void OnDisable()
    {
        _enemy.GetDamage -= PlayGetDamage;
    }

    private void PlayGetDamage()
    {
        _animator.SetTrigger("GetDamage");
    }
}
