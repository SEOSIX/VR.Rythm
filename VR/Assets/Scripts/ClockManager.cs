using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClockManager : MonoBehaviour
{
    
    public static ClockManager instance{get; private set;}
    public static event Action OnLoopComplete;

    void Awake()
    {
        instance = this;
    }
}
