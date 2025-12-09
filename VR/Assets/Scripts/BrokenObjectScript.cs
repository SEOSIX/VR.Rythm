using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
    public Renderer cameraLightRenderer;
    public Renderer rythmLightRenderer;
    public Renderer trapLightRenderer;
    public Slider fillingBar;
    
    [Header("Link Camera")] 
    public GameObject cameraCrash;
    public GameObject cameraDisplay;
    private bool fixingCameraCoroutIsRunning = false;
    
    [Header("Link Rythm Panel")] 
    public GameObject rythmPanelCrash;
    public GameObject rythmPanelDisplay;
    private bool fixingRymthCoroutIsRunning = false;

    public bool isCameraBroke = false;
    public bool isRythmPanelBroke = false;

    private bool isFixingSomething = false;
    
    void Start()
    {
        bipSource.clip = BIP;
        
        cameraLightRenderer.material = GreenMat;
        rythmLightRenderer.material = GreenMat;
        trapLightRenderer.material = GreenMat;

        bipSource.loop = false;

        fillingBar.maxValue = 100;
        fillingBar.value = 0;
    }

    public void Update()
    {
        CheckCamera();
        CheckRythmPanel();
    }

    private void CheckCamera()
    {
        if (isCameraBroke && fixingCameraCoroutIsRunning)
        {
            cameraDisplay.SetActive(false);
            cameraCrash.SetActive(true);
            cameraLightRenderer.material = OrangeMat;
        }
        else if (isCameraBroke)
        {
            cameraDisplay.SetActive(false);
            cameraCrash.SetActive(true);
            cameraLightRenderer.material = RedMat;
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
        if (isRythmPanelBroke && fixingRymthCoroutIsRunning)
        {
            rythmPanelDisplay.SetActive(false);
            rythmPanelCrash.SetActive(true);
            rythmLightRenderer.material = OrangeMat;
        }
        else if (isRythmPanelBroke)
        {
            rythmPanelDisplay.SetActive(false);
            rythmPanelCrash.SetActive(true);
            rythmLightRenderer.material = RedMat;
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
        }

        #region Camera
        
            public void RebootCameraButon()
            {
                if (!isCameraBroke || fixingCameraCoroutIsRunning || isFixingSomething)
                {
                    return;
                }
                
                StartCoroutine(RebootCameraCorout());
            }

            private IEnumerator RebootCameraCorout()
            {
                fixingCameraCoroutIsRunning = true;
                isFixingSomething = true;
                
                bipSource.Play();

                yield return StartCoroutine(SliderFillingCorout());

                displayError.timeToDisplayCamera = displayError.baseTimeToDisplayCamera;
                displayError.TimeDisplay.value = displayError.timeToDisplayCamera;
                isCameraBroke = false;
                displayError.CameraIsBroke = false;
                
                fixingCameraCoroutIsRunning = false;
                isFixingSomething = false;
            }

        #endregion
        
        #region Rythm
        
            public void RebootRythmButon()
            {
                if (!isRythmPanelBroke || fixingRymthCoroutIsRunning || isFixingSomething)
                {
                    return;
                }
                
                StartCoroutine(RebootRythmCorout());
            }

            private IEnumerator RebootRythmCorout()
            {
                fixingRymthCoroutIsRunning = true;
                isFixingSomething = true;
                
                bipSource.Play();
                
                yield return StartCoroutine(SliderFillingCorout());

                displayError.numberUsageRythmActivator = displayError.baseNumberUsageRythmActivator;
                isRythmPanelBroke = false;
                displayError.PanelDisplayIsBroke = false;
                
                fixingRymthCoroutIsRunning = false;
                isFixingSomething = false;
            }

        #endregion
        
        #region BreakAll
        
            public void BreakAll()
            {
                displayError.CameraIsBroke = true;
                displayError.PanelDisplayIsBroke = true;
            }

        #endregion
        
        IEnumerator SliderFillingCorout()
        {
            float duration = 5f;
            fillingBar.value = 0;

            while (fillingBar.value < fillingBar.maxValue)
            {
                fillingBar.value += fillingBar.maxValue * Time.deltaTime / duration;
                yield return null;
            }

            fillingBar.value = 0;
        }

    #endregion
}
