using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Искатель таргетов
/// </summary>
public class Seeker : State
{
    public float ViewRadius;
    public LayerMask TargetMask;
    public float CoolDown;

    protected NavMeshHit hit;

    delegate void Finder();

    Finder currentFinder;

    void FindTarget()
    {
        Vector3 position = transform.position;
        Collider[] targetsInRadius = Physics.OverlapSphere(position, ViewRadius, TargetMask);

        for (int i = 0; i < targetsInRadius.Length; i++)
        {
            if (!NavMesh.Raycast(position, targetsInRadius[i].transform.position, out hit, NavMesh.AllAreas))
            {
                guardian.Target = targetsInRadius[i].transform.parent;
                currentFinder = TraceTarget;
                guardian.SetState(StateID.persecute);
                break; // преследуем только первого видимого врага в списке.
            }
        }
    }

    void TraceTarget()
    {
        if (NavMesh.Raycast(transform.position, guardian.Target.transform.position, out hit, NavMesh.AllAreas))
        {
            guardian.Target = null;
            guardian.SetState(StateID.checkUp);
            currentFinder = FindTarget;
        }
        else
        {
            guardian.UpdateMemory();
        }
    }

    public void SetFindModeTrace()
    {
        currentFinder = TraceTarget;
    }

    public void SetFindModeFind()
    {
        currentFinder = FindTarget;
    }

    IEnumerator FindTargets()
    {
        while (true)
        {
            yield return new WaitForSeconds(CoolDown);
            currentFinder();
        }
    }

    public override void Enter()
    {
        currentFinder = FindTarget;
        StartCoroutine(FindTargets());
    }

    public override void Exit()
    {
        StopCoroutine(FindTargets());
    }
}
