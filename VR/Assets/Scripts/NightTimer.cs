using TMPro;
using UnityEngine;

public class NightTimer : MonoBehaviour
{
    public TMP_Text timerText;
    public Light spotLight;

    [Header("Timer Settings")]
    public float nightDurationInSeconds = 360f; 
    // 360s = 6 min -> 1 min = 1h de nuit (modifiable)

    public float currentTime = 0f;
    private float startIntensity;

    void Start()
    {
        startIntensity = spotLight.intensity;
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        
        float hours = Mathf.Lerp(0f, 6f, currentTime / nightDurationInSeconds);
        float clampedHours = Mathf.Clamp(hours, 0f, 6f);
        
        int h = Mathf.FloorToInt(clampedHours);
        int m = Mathf.FloorToInt((clampedHours - h) * 60);

        timerText.text = $"{h:00}:{m:00}";

        //  DIMINUTION DE LA LUMIÈRE ENTRE 5h ET 6h 
        if (clampedHours >= 1f)
        {
            float t = Mathf.InverseLerp(1f, 5f, clampedHours);  
            spotLight.intensity = Mathf.Lerp(startIntensity, 0f, t);
        }

        //Reset
        /*
        if (currentTime >= nightDurationInSeconds)
        {
            currentTime = 0f;
            spotLight.intensity = startIntensity;
        }
        */
    }
}