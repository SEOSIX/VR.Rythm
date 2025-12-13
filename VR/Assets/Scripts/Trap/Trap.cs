using System;
using System.Collections;
using DefaultNamespace;
using UnityEngine;

[System.Serializable]
public class TrapDisplayPattern
{
    public GameObject[] imagePrefabs;
    public int numberToSpawn;
    public float customFallSpeed;
    [Header("Intervalle entre les apparitions")]
    public float spawnInterval;
}

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

    
    
    [Header("Pattern visuel du piège")]
    public TrapDisplayPattern displayPattern;
    

    public virtual void ActivateTrap()
    {
        if (DisplayManager.instance != null)
        {
            DisplayManager.instance.DisplayCustomPattern(displayPattern);
        }

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
