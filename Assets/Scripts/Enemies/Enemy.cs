using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyWeapon))]
[RequireComponent(typeof(Player))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyWeapon _weapon;
    [SerializeField] private Player _target;
    [SerializeField] private int _health;

    public Player Target => _target;
    public EnemyWeapon Weapon=> _weapon;

    public event UnityAction<Enemy> Dying;
    public event UnityAction GetDamage;

    private AudioSource _audio;

    private Animator _animator;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        SetTargetForWeapons();
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        GetDamage?.Invoke();
        _animator.SetTrigger("GetDamage");
        _audio.Play();

        if (_health <= 0)
        {
            Dying?.Invoke(this);
            Destroy(gameObject);
        }
    }

    private void SetTargetForWeapons()
    {
          _weapon.SetTarget(_target);
    }
}
