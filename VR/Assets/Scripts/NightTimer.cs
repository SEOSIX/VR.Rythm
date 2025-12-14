using System.Collections;
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

    void Start()
    {
        currentTime = nightDurationInSeconds;
        startIntensity = spotLight.intensity;
    }

    // gaffe au code en update
    void Update()
    {
        if (canDecreaseTime)
        {
            canDecreaseTime = false;
            StartCoroutine(DecreaseTimeCorout());
        }

        spotLight.intensity = startIntensity * (currentTime / 100);
        
        if (currentTime <= 0f)
        {
            SceneManager.LoadScene(0);
        }
    }

    IEnumerator DecreaseTimeCorout()
    {

        yield return new WaitForSeconds(1f);

        currentTime -= 1;
        canDecreaseTime = true;
        timerText.text = $"{currentTime}";

    }
}