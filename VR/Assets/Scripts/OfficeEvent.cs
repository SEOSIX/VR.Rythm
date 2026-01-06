using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class OfficeEvent : MonoBehaviour
{

    public MusicManager musicManager;
    
    [Header("Lights part")]
    public Light officeLight;
    public GameObject leftLight;
    public GameObject rightLight;
    public GameObject midleLight;

    [Header("Sound Clip")] 
    public AudioClip musicDeFond;
    public AudioClip lightFlicker;

    public AudioSource MusiqueSource;
    
    
    private bool isFlickering = false;
    private float CheckFlickering = 0;

    private void Start()
    {

        RenderSettings.fogDensity = 0.300f;
        
        //Light
        leftLight.SetActive(false);
        rightLight.SetActive(false);
        midleLight.SetActive(false);
        
        //Music
        musicManager.PlayMusic(MusiqueSource,musicDeFond,true);
        
    }

    private void Update()
    {

        FlickeringCheck();
        
    }

    private void FlickeringCheck()
    {
        if (!(Time.time - CheckFlickering < 0.1 ))
        {
            return;
        }

        CheckFlickering = Time.time;
        
        Flicker();

    }

    private void Flicker()
    {
        
        if (!isFlickering)
        {
            if (Random.Range(0f, 1f) < 0.0001f) // 0.1% de chance par frame
            {
                StartCoroutine(FlickerCoroutine(Random.Range(2, 20)));
            }
        }
        
    }

    private IEnumerator FlickerCoroutine(int flickers)
    {
        isFlickering = true;
        musicManager.PlaySoundEffect(lightFlicker);
        
        for (int i = 0; i < flickers; i++)
        {
            officeLight.enabled = false;
            yield return new WaitForSeconds(Random.Range(0.05f, 0.3f));
            officeLight.enabled = true;
            yield return new WaitForSeconds(Random.Range(0.05f, 0.3f));
        }
        
        yield return new WaitForSeconds(20f);
        isFlickering = false;
    }
}