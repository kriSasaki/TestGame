using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    private const string GetDamage = "GetDamage";

    [SerializeField] private int _health;
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private static SpriteRenderer _spriteRenderer;
    [SerializeField] private AudioSource _applyDamage;

    public bool Use => _use;

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<Sprite> WeaponChanged;

    protected Weapon _currentWeapon;

    private int _currentWeaponNumber = 0;
    private int _currentHealth;

    private PlayerInput _input;

    private Animator _animator;

    private float _mouseScrollY;
    private bool _shoot => _input.Player.Shoot.IsPressed();
    private bool _use => _input.Player.Use.IsPressed();
    public string Layer => _spriteRenderer.sortingLayerName;

    private void Awake()
    {
        _input = new PlayerInput();
        _input.Player.ChangeWeapon.performed += x => _mouseScrollY = x.ReadValue<float>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _currentHealth = _health;
        GenerateStartWeapons();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Update()
    {
        if (_shoot)
        {
            Shoot();
        }

        if (_mouseScrollY > 0)
        {
            NextWeapon();
        }
        if (_mouseScrollY < 0)
        {
            PreviousWeapon();
        }
    }

    public void AddWeapon(Weapon newWeapon)
    {
        _weapons.Add(newWeapon);
        _weapons[_weapons.Count - 1] = Instantiate(_weapons[_weapons.Count - 1], gameObject.transform);
        _weapons[_weapons.Count - 1] = SetWeaponVisible(_weapons[_weapons.Count - 1], false);
    }

    public void ApplyDamage(int damage)
    {
        _applyDamage.Play();
        _currentHealth -= damage;
        HealthChanged?.Invoke(_currentHealth, _health);
        _animator.SetTrigger(GetDamage);

        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void ApplyHeal(int heal)
    {
        if (_currentHealth + heal <= _health)
        {
            _currentHealth += heal;
        }
        else
        {
            _currentHealth = _health;
        }

        HealthChanged?.Invoke(_currentHealth, _health);
    }

    public void Shoot()
    {
        _currentWeapon.Shoot();
    }

    public void SetWeaponsPositionZ(int z)
    {
        foreach (var weapon in _weapons)
        {
            weapon.SetPositionZ(z);
        }
    }

    private void NextWeapon()
    {
        if (_currentWeaponNumber == _weapons.Count - 1)
        {
            _currentWeaponNumber = 0;
        }
        else
        {
            _currentWeaponNumber++;
        }

        ChangeWeapon(_weapons[_currentWeaponNumber]);
    }

    private void PreviousWeapon()
    {
        if (_currentWeaponNumber == 0)
        {
            _currentWeaponNumber = _weapons.Count - 1;
        }
        else
        {
            _currentWeaponNumber--;
        }

        ChangeWeapon(_weapons[_currentWeaponNumber]);
    }

    private void ChangeWeapon(Weapon weapon)
    {
        _currentWeapon = SetWeaponVisible(_currentWeapon, false); ;
        _currentWeapon = weapon;
        _currentWeapon = SetWeaponVisible(_currentWeapon, true);
        WeaponChanged?.Invoke(_currentWeapon.Icon);
    }

    private void GenerateStartWeapons()
    {
        for (int i = 0; i < _weapons.Count; i++)
        {
            _weapons[i] = Instantiate(_weapons[i], gameObject.transform);
            _weapons[i] = SetWeaponVisible(_weapons[i], false);
        }

        _currentWeapon = _weapons[_currentWeaponNumber];
        _currentWeapon = SetWeaponVisible(_currentWeapon, true);
        WeaponChanged?.Invoke(_currentWeapon.Icon);
    }

    private Weapon SetWeaponVisible(Weapon weapon, bool isHide)
    {
        weapon.GetComponent<Weapon>().enabled = isHide;
        weapon.GetComponent<SpriteRenderer>().enabled = isHide;

        return weapon;
    }
}
