using System.Collections;
using UnityEngine;



public class DisplayError : MonoBehaviour
{
    public enum ErrorType
    {
        Camera,
        PanelDisplay,
        Door
    }
    public ErrorType typeError;

    [SerializeField]private float timeToDisplayCamera;
    [SerializeField] private Canvas cameraCanvasDisplay;


    [SerializeField] private int numberUsageRythmActivator;
    
    void Update()
    {
        Timer(timeToDisplayCamera);
    }



    public void Timer(float timeToDisplay)
    {
        timeToDisplay -= Time.deltaTime;

        if (timeToDisplay <= 0f)
        {
            typeError = ErrorType.Camera;
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
                Debug.Log("ErrorPanel");
                break;
        }
    }
}