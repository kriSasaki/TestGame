using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody2D))]
public class PatrolState : State
{
    [SerializeField] private float _patrolSpeed;
    [SerializeField] private Transform _path;

    private NavMeshAgent _agent;

    private Transform[] _points;

    private int _currentPosition = 0;

    private LookDirection _lookDirection;

    private void Start()
    {
        _lookDirection= GetComponent<LookDirection>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _points = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
        {
            _points[i] = _path.GetChild(i);
        }

        _agent.SetDestination(new Vector3(_points[_currentPosition].position.x, _points[_currentPosition].position.y, transform.position.z));
        _lookDirection.SetLookDirection(_points[_currentPosition].position);
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, _points[_currentPosition].position) <=0.1f)
        {
            if (_currentPosition == _points.Length - 1)
            {
                _currentPosition = 0;
                _agent.SetDestination(new Vector3(_points[_currentPosition].position.x, _points[_currentPosition].position.y, transform.position.z));
                _lookDirection.SetLookDirection(_points[_currentPosition].position);
            }
            else
            {
                _currentPosition += 1;
                _agent.SetDestination(new Vector3(_points[_currentPosition].position.x, _points[_currentPosition].position.y, transform.position.z));
                _lookDirection.SetLookDirection(_points[_currentPosition].position);
            }
        }
    }
}
