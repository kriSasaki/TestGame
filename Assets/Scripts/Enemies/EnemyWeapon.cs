using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class EnemyWeapon : MonoBehaviour
{
    [SerializeField] protected Player _target;
    [SerializeField] protected Animator _animator;
    [SerializeField] protected AudioSource _audio;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    public abstract void Attack();

    public void SetTarget(Player target)
    {
        _target = target;
    }
}
