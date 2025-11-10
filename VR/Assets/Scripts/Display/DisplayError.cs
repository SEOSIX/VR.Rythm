using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class DisplayError : MonoBehaviour
{
    public static DisplayError instance { get; private set; }
    
    
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
    public Canvas cameraCanvasDisplay;
    public Slider TimeDisplay;
    
    [Header("Rhythm Settings")]
    public int baseNumberUsageRythmActivator; 
    public int numberUsageRythmActivator;
    public Canvas rythmsCanvasDisplay;


    /// TEMPORAIRE ///
    

    

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
        TimeDisplay.value = TimeDisplay.maxValue;
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

                cameraCanvasDisplay.enabled = false;
                //Afficher l'erreur au terminal de contrôle stp mercii
                Debug.Log("ErrorCamera");
                break;
            case (ErrorType.Door) :
                Debug.Log("ErrorDoor");
                break;
            case (ErrorType.PanelDisplay) :
                rythmsCanvasDisplay.enabled = false;
                //same here t'a que récupérer l'autre truc qui permet de 
                Debug.Log("ErrorPanel");
                break;
        }
    }
}