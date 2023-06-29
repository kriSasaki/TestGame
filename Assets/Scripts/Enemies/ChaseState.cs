using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State
{
    private NavMeshAgent _agent;

    private LookDirection _lookDirection;

    private void Start()
    {
        _lookDirection= GetComponent<LookDirection>();
         _agent= GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    private void Update()
    {
        _agent.SetDestination(new Vector3(Target.transform.position.x, Target.transform.position.y, transform.position.z));
        _lookDirection.SetLookDirection(Target.transform.position);
    }
}
