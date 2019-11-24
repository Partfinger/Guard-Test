using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void GuardianUpdater(StateID id);
public enum StateID { patrol = 1, persecute };

public class Guardian : MonoBehaviour
{
    public State[] states;
    [SerializeField]
    StateID current = StateID.patrol;

    [SerializeField]
    Transform target;
    public Vector3 memory;

    public Transform Target
    {
        get
        {
            return target;
        }
        set
        {
            if (value)
            {
                target = value;
                memory = target.transform.position;
            }
            else
            {
                memory = target.position;
            }
        }
    }

    private void Start()
    {
        for (int i = 0; i < states.Length; i++)
        {
            states[i].guardian = this;
        }
        states[0].Enter();
        states[(byte)current].Enter();
    }

    /// <summary>
    /// Как-бы конечный автомат
    /// </summary>
    /// <param name="newState"></param>
    public void SetState(StateID newState)
    {
        if (newState == current)
            return;
        states[(byte)current].Exit();
        current = newState;
        states[(byte)current].Enter();
    }

    public bool PointReached()
    {
        if (!((transform.position - target.position).sqrMagnitude < 0.25f))
        {
            SetState(StateID.patrol);
            return false;
        }
        return true;
    }
}
