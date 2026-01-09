using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NightTimer : MonoBehaviour
{
    public static NightTimer singleton { get; private set; }
    
    public TMP_Text timerText;
    public Light spotLight;

    [Header("Timer Settings")]
    public float nightDurationInSeconds = 360f;

    public float currentTime;
    public bool isTimerRunning; 
    private float startIntensity;

    public List<GameObject> tutorialText = new List<GameObject>();
    public bool hasBeenDeactivated = false;

    private void Awake()
    {
        singleton = this;
    }

    void Start()
    {
        currentTime = nightDurationInSeconds;
        startIntensity = spotLight.intensity;
        UpdateUI();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            
            currentTime -= Time.deltaTime;

            if (currentTime <= 0f)
            {
                currentTime = 0f;
                CheckTime();
            }

            UpdateUI();
            CheckTutorial();
        }
    }

    private void UpdateUI()
    {
        // On utilise Mathf.CeilToInt pour afficher "10" tant qu'on est à 9.1 par exemple
        timerText.text = Mathf.CeilToInt(currentTime).ToString();
    }
    
    public void CheckTime()
    {
        if (currentTime <= 0f)
        {
            SceneManager.LoadScene(3);
        }
    }

    private void CheckTutorial()
    {
        if (!hasBeenDeactivated && currentTime <= (nightDurationInSeconds - 60f))
        {
            DeactivateTuto();
        }
    }

    public void DeactivateTuto()
    {
        foreach (var obj in tutorialText)
        {
            if(obj != null) obj.SetActive(false);
        }
        hasBeenDeactivated = true;
    }
}