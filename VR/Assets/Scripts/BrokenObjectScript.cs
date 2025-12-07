using System.Collections;
using UnityEngine;

public class BrokenObjectScript : MonoBehaviour
{

    [Header("Genral link")] 
    public Material GreenMat;
    public Material OrangeMat;
    public Material RedMat;

    public AudioClip BIP;
    public AudioSource bipSource;
    
    public DisplayError displayError;
    
    [Header("Link General fixing device")] 
    //public GameObject deviceScreen;
    public Renderer cameraLightRenderer;
    public Renderer rythmLightRenderer;
    public Renderer trapLightRenderer;
    
    [Header("Link Camera")] 
    public GameObject cameraCrash;
    public GameObject cameraDisplay;
    private bool fixingCameraCoroutIsRunning = false;
    
    [Header("Link Rythm Panel")] 
    public GameObject rythmPanelCrash;
    public GameObject rythmPanelDisplay;
    private bool fixingRymthCoroutIsRunning = false;
    
    /*
    [Header("Link Trap")] 
    public GameObject trapCrash;
    public GameObject trapDisplay;
    */
    
     public bool isCameraBroke = false;
     public bool isRythmPanelBroke = false;
    //[HideInInspector] public bool isTrapBroke = false;
    
    void Start()
    {
        //deviceScreen.SetActive(false);
        bipSource.clip = BIP;
        
        cameraLightRenderer.material = GreenMat;
        rythmLightRenderer.material = GreenMat;
        trapLightRenderer.material = GreenMat;

        bipSource.loop = false;

    }
    public void Update()
    {

        CheckCamera();
        CheckRythmPanel();
     

    }

    private void CheckCamera()
    {
        if (isCameraBroke)
        {
            cameraDisplay.SetActive(false);
            cameraCrash.SetActive(true);
            
            cameraLightRenderer.material = RedMat;
        }
        else if (isCameraBroke && fixingCameraCoroutIsRunning)
        {
            cameraDisplay.SetActive(false);
            cameraCrash.SetActive(true);
            
            cameraLightRenderer.material = OrangeMat;
        }
        else
        {
            cameraDisplay.SetActive(true);
            cameraCrash.SetActive(false);
           
            cameraLightRenderer.material = GreenMat;
        }
    }

    private void CheckRythmPanel()
    {
        if (isRythmPanelBroke)
        {
            rythmPanelDisplay.SetActive(false);
            rythmPanelCrash.SetActive(true);

            rythmLightRenderer.material = RedMat;
        }
        else if (isRythmPanelBroke && fixingRymthCoroutIsRunning)
        {
            rythmPanelDisplay.SetActive(false);
            rythmPanelCrash.SetActive(true);

            rythmLightRenderer.material = OrangeMat;
        }
        else
        {
            rythmPanelDisplay.SetActive(true);
            rythmPanelCrash.SetActive(false);

            rythmLightRenderer.material = GreenMat;
        }
    }
    

    #region FixingStuff

        public void TurnOnAndOffFixingDevice()
        {
            //deviceScreen.SetActive(!deviceScreen.activeSelf);
            //Si besoin faire la degradation de la batterie ici if(deviceScreen.activeSelf)
        }

        #region Camera
        
            // Activated by buton in game
            public void RebootCameraButon()
            {
                if (!isCameraBroke || fixingCameraCoroutIsRunning)
                {
                    return;
                }
                
                StartCoroutine(RebootCameraCorout(Random.Range(7, 20)));
            }
            private IEnumerator RebootCameraCorout(int RebootTime)
            {
                
                fixingCameraCoroutIsRunning = true;
                
                bipSource.Play();
                
                yield return new WaitForSeconds(RebootTime);
                displayError.timeToDisplayCamera = displayError.baseTimeToDisplayCamera;
                displayError.TimeDisplay.value = displayError.timeToDisplayCamera;
                isCameraBroke = false;
                
                //changer avec le bin mask
                displayError.CameraIsBroke = false;
                
                fixingCameraCoroutIsRunning = false;
            }

        #endregion
        
        #region Rythm
        
            // Activated by buton in game
            public void RebootRythmButon()
            {
                if (!isRythmPanelBroke)
                {
                    return;
                }
                
                StartCoroutine(RebootRythmCorout(Random.Range(7, 20)));
            }
            private IEnumerator RebootRythmCorout(int RebootTime)
            {
                fixingRymthCoroutIsRunning = true;
                
                bipSource.Play();
                
                yield return new WaitForSeconds(RebootTime);
                displayError.numberUsageRythmActivator = displayError.baseNumberUsageRythmActivator;
                isRythmPanelBroke = false;
                
                //changer avec le bin mask
                displayError.PanelDisplayIsBroke = false;
                
                fixingRymthCoroutIsRunning = false;
            }

        #endregion
        
        /*
        #region Trap
        
            // Activated by buton in game
            public void RebootTrapButon()
            {
                if (!isTrapBroke)
                {
                    return;
                }
                
                StartCoroutine(RebootCameraCorout(Random.Range(7, 20)));
            }
            private IEnumerator RebootTrapCorout(int RebootTime)
            {
                yield return new WaitForSeconds(RebootTime);
                isTrapBroke = false;
            }

        #endregion
        */

    #endregion
    
    
    
    
}
