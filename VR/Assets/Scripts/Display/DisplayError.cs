using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class DisplayError : MonoBehaviour
{
    public static DisplayError instance { get; private set; }
    public BrokenObjectScript brokenObjectScript;
    
    public enum ErrorType
    {
        Camera,
        PanelDisplay,
        Door
    }
    public ErrorType typeError;

    [Header("Camera Settings")]
    public float baseTimeToDisplayCamera;
    public float timeToDisplayCamera; 
    
    [Header("Rhythm Settings")]
    public int baseNumberUsageRythmActivator; 
    public int numberUsageRythmActivator;


    /// TEMPORAIRE ///
    public Slider TimeDisplay;

    


    private void Awake()
    {
        instance = this;
    }
    
    private void Start()
    {
        baseTimeToDisplayCamera = timeToDisplayCamera;
        baseNumberUsageRythmActivator = numberUsageRythmActivator;

        if (TimeDisplay != null)
            TimeDisplay.maxValue = timeToDisplayCamera;
    }

    void Update()
    {
        Timer(timeToDisplayCamera);
    }
    
    
    public void Timer(float timeToDisplay)
    {
        timeToDisplay -= Time.deltaTime;
        TimeDisplay.value--;
        if (timeToDisplay <= 0f)
        {
            typeError = ErrorType.Camera;
        }
    }


    public void DecreaseUsageRythms(int usages)
    {
        usages --;
        if (usages <= 0)
        {
            typeError = ErrorType.PanelDisplay;
        }
    }
    
    private void ReportBug()
    {
        switch (typeError)
        {
            case (ErrorType.Camera) :
                brokenObjectScript.isCameraBroke = true;
                break;
            
            case (ErrorType.Door) :
                brokenObjectScript.isTrapBroke = true;
                break;
            
            case (ErrorType.PanelDisplay) :
                brokenObjectScript.isRythmPanelBroke = true;
                break;
        }
    }
}