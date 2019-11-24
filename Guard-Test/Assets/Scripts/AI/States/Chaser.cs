using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/// <summary>
/// Идти к целе
/// </summary>
public class Chaser : State
{
    [SerializeField]
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public override void Enter()
    {
        base.Enter();
        agent.isStopped = false;
    }

    void Update()
    {
        agent.destination = guardian.memory;
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            agent.isStopped = guardian.PointReached();
        }
    }
}
