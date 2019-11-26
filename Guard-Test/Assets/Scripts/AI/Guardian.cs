using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void GuardianUpdater(StateID id);
public enum StateID { patrol = 1, persecute, checkUp };

public class Guardian : MonoBehaviour
{
    public State[] states;
    StateID current = StateID.patrol;
    State currentState;

    public Material material;
    public Color[] stateColor =
    {
        Color.green,
        Color.yellow,
        Color.red
    };

    public Vector3 memory;

    public Transform Target;

    private void Start()
    {
        for (int i = 0; i < states.Length; i++)
        {
            states[i].guardian = this;
        }
        currentState = states[1];
        currentState.Enter();
        states[0].Enter();
        material.color = stateColor[0];
    }

    /// <summary>
    /// Как-бы конечный автомат
    /// </summary>
    /// <param name="newState"></param>
    public void SetState(StateID newState)
    {
        if (newState == current)
            return;
        current = newState;
        currentState.Exit();
        switch(current)
        {
            case StateID.patrol:
                currentState = states[1];
                material.color = stateColor[0];
                break;
            case StateID.persecute:
                currentState = states[2];
                material.color = stateColor[2];
                break;
            case StateID.checkUp:
                currentState = states[2];
                material.color = stateColor[1];
                break;
        }
        currentState.Enter();
    }

    public bool PointReached()
    {
        if (Target)
        {
            if (!((transform.position - Target.position).sqrMagnitude < 1f))
            {
                SetState(StateID.patrol);
                return false;
            }
            else
            {
                Target.GetComponentInParent<Player>().Stop();
                return true;
            }
        }
        else
        {
            SetState(StateID.patrol);
            return false;
        }
    }

    public void UpdateMemory()
    {
        memory = Target.position;
    }

    public void Stop()
    {
        enabled = false;
        states[0].Exit();
        currentState.Exit();
    }
}
