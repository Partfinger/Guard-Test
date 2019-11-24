using UnityEngine;

public abstract class State : MonoBehaviour
{
    public Guardian guardian;

    public virtual void Enter()
    {
        enabled = true;
    }

    public virtual void Exit()
    {
        enabled = false;
    }

    public virtual void Subscribe(GuardianUpdater updater)
    {
        return;
    }
}
