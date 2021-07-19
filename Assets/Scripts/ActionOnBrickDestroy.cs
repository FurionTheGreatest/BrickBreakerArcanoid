using System;
using UnityEngine;

public class ActionOnBrickDestroy : MonoBehaviour
{
    public ActionTarget actionTarget;

    public void DoAction()
    {
        switch (actionTarget)
        {
            case ActionTarget.PaddleSizeIncrease:
                StartChanges<PaddleSizeIncreaser>(7);
                break;
            case ActionTarget.BallSlowDown:
                StartChanges<BallSlowDown>(10);
                break;
            case ActionTarget.BallInstantKill:
                StartChanges<BallDamage>(10);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void StartChanges<T>(float duration) where T : BaseStatsChanger
    {
        var changeable = FindObjectOfType<T>();
        changeable.StartChangesRoutine(duration);
    }
    
}
public enum ActionTarget
{
    PaddleSizeIncrease,
    BallSlowDown,
    BallInstantKill
}
