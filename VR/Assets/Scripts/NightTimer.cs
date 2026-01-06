using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NightTimer : MonoBehaviour
{
    public TMP_Text timerText;
    public Light spotLight;

    [Header("Timer Settings")]
    public float nightDurationInSeconds = 360f;

    public float currentTime;
    private bool canDecreaseTime = true;
    private float startIntensity;

    public List<GameObject> tutorialText = new List<GameObject>();
    public bool hasBeenDeactivated = false;
    
    void Start()
    {
        currentTime = nightDurationInSeconds;
        startIntensity = spotLight.intensity;
    }

    void Update()
    {
        if (canDecreaseTime)
        {
            canDecreaseTime = false;
            StartCoroutine(DecreaseTimeCorout());
        }

        CheckTime();
        
        LightIntensity();


        if (!hasBeenDeactivated && Mathf.Approximately(currentTime, nightDurationInSeconds - 60f))
        {
            DeactivateTuto();
        }
        
    }
    
    IEnumerator DecreaseTimeCorout()
    {

        yield return new WaitForSeconds(1f);

        currentTime -= 1;
        canDecreaseTime = true;
        timerText.text = $"{currentTime}";

    }
    
    public void CheckTime()
    {
        if (currentTime <= 0f)
        {
            SceneManager.LoadScene(3);
        }
    }

    public void LightIntensity()
    {
        
        spotLight.intensity = startIntensity * (currentTime / 100);
        
    }

    public void DeactivateTuto()
    {
        foreach (var VARIABLE in tutorialText)
        {
            VARIABLE.SetActive(false);
        }

        hasBeenDeactivated = true;
        
    }

    
}