using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AttackState : State
{
    [SerializeField] private int _damage;
    [SerializeField] private float _delay;
    [SerializeField] private EnemyWeapon _weapon;

    private float _lastAttackTime = 0;

    private LookDirection _lookDirection;

    private void Start()
    {
        _lookDirection = GetComponent<LookDirection>();
    }

    private void Update()
    {
        _lookDirection.SetLookDirection(Target.transform.position);

        if (_lastAttackTime <= 0)
        {
            Attack(Target);

            _lastAttackTime = _delay;
        }

        _lastAttackTime -= Time.deltaTime;
    }

    private void Attack(Player target)
    {
        _weapon.Attack();
        target.ApplyDamage(_damage);
    }
}