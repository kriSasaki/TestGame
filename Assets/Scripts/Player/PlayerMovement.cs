using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    private const string Direction = "Direction";

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _takeDistance;
    [SerializeField] private float _holdDistance;

    private PlayerInput _input;

    private GetValueZ _getValueZ;

    private Player _player;

    private Animator _animator;

    private Rigidbody2D _rigidbody2D;

    private Vector2 _moveDirection;

    private float _lookAngle;
    private float _angle45 = 45;
    private float _angle135 = 135;
    private float _angle225 = -135;
    private float _angle315 = -45;

    private Side _side;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _input = new PlayerInput();
        _input.Enable();
    }

    private void Start()
    {
        _getValueZ = GetComponent<GetValueZ>();
        _animator = GetComponent<Animator>();
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        _moveDirection = _input.Player.Move.ReadValue<Vector2>();

        if (_moveDirection != Vector2.zero)
        {
            Move(_moveDirection);
        }
        else
        {
            _rigidbody2D.velocity = Vector2.zero;
        }

        _lookAngle = _getValueZ.GetValue(out Vector2 abscissaAxis, out Vector2 viewVector) * (int)GetSide(abscissaAxis, viewVector);

        if (_lookAngle > _angle315 && _lookAngle < _angle45)
        {
            _animator.SetInteger(Direction, 2);
        }

        if (_lookAngle > _angle45 && _lookAngle < _angle135)
        {
            _animator.SetInteger(Direction, 1);
            _player.SetWeaponsPositionZ(0);
        }
        else
        {
            _player.SetWeaponsPositionZ(-1);
        }

        if (_lookAngle > _angle135 || _lookAngle < _angle225)
        {
            _animator.SetInteger(Direction, 3);
        }

        if (_lookAngle > _angle225 && _lookAngle < _angle315)
        {
            _animator.SetInteger(Direction, 0);
        }
    }

    private void Move(Vector2 direction)
    {
        if (direction.sqrMagnitude < 0.1)
        {
            return;
        }

        Vector2 move = Quaternion.Euler(transform.eulerAngles.y, 0, 0) * new Vector2(direction.x, direction.y);
        _rigidbody2D.velocity = _moveSpeed * move;
    }

    private Side GetSide(Vector2 abscissaAxis, Vector2 viewVector)
    {
        _side = Side.Right;

        if (viewVector.y <= abscissaAxis.y)
        {
            _side = Side.Left;
        }

        return _side;
    }

    private enum Side
    {
        Left = -1,
        Right = 1
    }
}
