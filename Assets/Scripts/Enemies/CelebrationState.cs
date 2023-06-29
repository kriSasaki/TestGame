using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CelebrationState : State
{
    private const string PlayerDie = "PlayerDie";

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animator.SetBool(PlayerDie, true);
    }

    private void OnDisable()
    {
        _animator.StopPlayback();
    }
}
