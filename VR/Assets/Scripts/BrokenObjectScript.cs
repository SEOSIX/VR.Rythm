using System.Collections;
using UnityEngine;

public class BrokenObjectScript : MonoBehaviour
{

    [Header("Genral link")] 
    public Material GreenMat;
    public Material RedMat;
    
    [Header("Link General fixing device")] 
    //public GameObject deviceScreen;
    public Renderer cameraLightRenderer;
    public Renderer rythmLightRenderer;
    public Renderer trapLightRenderer;
    
    [Header("Link Camera")] 
    public GameObject cameraCrash;
    public GameObject cameraDisplay;
    
    [Header("Link Rythm Panel")] 
    public GameObject rythmPanelCrash;
    public GameObject rythmPanelDisplay;
    
    
    [Header("Link Trap")] 
    public GameObject trapCrash;
    public GameObject trapDisplay;
    
    [HideInInspector] public bool isCameraBroke = false;
    [HideInInspector] public bool isRythmPanelBroke = false;
    [HideInInspector] public bool isTrapBroke = false;
    
    void Start()
    {
        //deviceScreen.SetActive(false);
        
        cameraLightRenderer.material = GreenMat;
        rythmLightRenderer.material = GreenMat;
        trapLightRenderer.material = GreenMat;
        
    }
    public void Update()
    {
        if (isCameraBroke)
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
        
        if (isRythmPanelBroke)
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
        
        if (isTrapBroke)
        {
            trapDisplay.SetActive(false);
            trapCrash.SetActive(true);

            trapLightRenderer.material = RedMat;
        }
        else
        {
            trapDisplay.SetActive(true);
            trapCrash.SetActive(false);

            trapLightRenderer.material = GreenMat;
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
                if (!isCameraBroke)
                {
                    return;
                }
                
                StartCoroutine(RebootCameraCorout(Random.Range(7, 20)));
            }
            private IEnumerator RebootCameraCorout(int RebootTime)
            {
                yield return new WaitForSeconds(RebootTime);
                isCameraBroke = false;
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
                yield return new WaitForSeconds(RebootTime);
                isRythmPanelBroke = false;
            }

        #endregion
        
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

    #endregion
    
    
    
    
}
