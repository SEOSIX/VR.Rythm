using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class DisplayError : MonoBehaviour
{
    public static DisplayError instance { get; private set; }
    public BrokenObjectScript brokenObjectScript;

    [HideInInspector] public bool CameraIsBroke = false;
    [HideInInspector] public bool PanelDisplayIsBroke = false;
    [HideInInspector] public bool Door = false;
    
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
        //typeError = ErrorType.none;
        baseTimeToDisplayCamera = timeToDisplayCamera;
        baseNumberUsageRythmActivator = numberUsageRythmActivator;

        // si le timeDisplay est egal à null, ca chiera 2 lignes apres,
        // soit le check sert à rien, soit ca puducu et ca fera des exceptions
        if (TimeDisplay != null)
            TimeDisplay.maxValue = baseTimeToDisplayCamera;
        TimeDisplay.value = timeToDisplayCamera;
    }

    // tu ne devrais avoir rien de spécifique dans l'update, le reportBug, c'est OK,
    // parcontre pour le computer.isOpen il faudrait avoir une fonction ManagerComputer par exemple, et mettre tout dedans
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
        // le timeDisplay, si t'as une bonne machine, il va dégringoler vachement vite,
        // et trigger cameraisBroke bien plus souvent nan?
        timeToDisplay -= Time.deltaTime;
        TimeDisplay.value--;
        if (TimeDisplay.value <= 0f)
        {
            //typeError = ErrorType.Camera;
            CameraIsBroke = true;
        }
    }


    public void DecreaseUsageRythms(int usages)
    {
        usages --;
        if (usages <= 0)
        {
            //typeError = ErrorType.PanelDisplay;
            PanelDisplayIsBroke = true;
        }
    }
    
    
    private void ReportBug()
    {
        if (CameraIsBroke)
        {
            brokenObjectScript.isCameraBroke = true;
        }

        if (PanelDisplayIsBroke)
        {
            brokenObjectScript.isRythmPanelBroke = true;
        }
        
    }
    
}