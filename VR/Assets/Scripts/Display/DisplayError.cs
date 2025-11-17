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
        none,
        Camera,
        PanelDisplay,
        Door
    }
    public ErrorType typeError;

    [Header("Camera Settings")]
    public float baseTimeToDisplayCamera;
    public float timeToDisplayCamera; 
    public Canvas cameraCanvasDisplay;
    public Slider TimeDisplay;
    
    [Header("Rhythm Settings")]
    public int baseNumberUsageRythmActivator; 
    public int numberUsageRythmActivator;



    

    private void Awake()
    {
        instance = this;
    }
    
    private void Start()
    {
        typeError = ErrorType.none;
        baseTimeToDisplayCamera = timeToDisplayCamera;
        baseNumberUsageRythmActivator = numberUsageRythmActivator;

        if (TimeDisplay != null)
            TimeDisplay.maxValue = baseTimeToDisplayCamera;
        TimeDisplay.value = timeToDisplayCamera;
    }

    void Update()
    {
		ReportBug();
        if (Computer.isOpen)
        {
            Timer(timeToDisplayCamera, true);
        }
        else
        {
            Timer(timeToDisplayCamera, false);
        }
    }
    
    
    public void Timer(float timeToDisplay, bool canDecrease)
    {
        if (!canDecrease)
            return;
        timeToDisplay -= Time.deltaTime;
        TimeDisplay.value--;
        if (TimeDisplay.value <= 0f)
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
                //brokenObjectScript.isTrapBroke = true;
                break;
            
            case (ErrorType.PanelDisplay) :
                brokenObjectScript.isRythmPanelBroke = true;
                break;
        }
    }
}