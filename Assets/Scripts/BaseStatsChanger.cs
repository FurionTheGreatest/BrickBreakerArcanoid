using System.Collections;
using UnityEngine;

public abstract class BaseStatsChanger : MonoBehaviour
{
    public void StartChangesRoutine(float changesDuration)
    {
        StartCoroutine(ChangeStats(changesDuration));
    }
    protected abstract void ActivateChanges();

    private IEnumerator ChangeStats(float changesTimer = 3f)
    {
        ActivateChanges();
        yield return new WaitForSeconds(changesTimer);
        ResetStats();
    }
    protected abstract void ResetStats();
}
