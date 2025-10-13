using System;
using System.Collections;
using UnityEngine;

public abstract class Trap : MonoBehaviour
{
    public string TramName;
    private enum Traps
    {
        wall,
        Trap2,
        Trap3
    }
    [SerializeField] private Traps trap;
    public int TrapDuration;


    public virtual void ActivateTrap()
    {
        StartCoroutine(DurationEnded());
    }

    public virtual void TrapTriggered()
    {
        
    }
    private IEnumerator DurationEnded()
    {
        yield return new WaitForSeconds(TrapDuration);
        OnDurationEnded(); 
    }

    public virtual void OnDurationEnded() {}
}
