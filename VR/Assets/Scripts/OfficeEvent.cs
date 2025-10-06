using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class OfficeEvent : MonoBehaviour
{
    [Header("Lights")]
    public Light officeLight;
    public GameObject leftLight;
    public GameObject rightLight;
    public GameObject midleLight;

    [Header("Sound Source")] 
    public AudioSource musiqueSource;
    public AudioSource soundEffectSource;

    [Header("Sound Clip")] 
    public AudioClip musicDeFond;
    public AudioClip lightFlicker;
    
    
    private bool isFlickering = false;

    private void Start()
    {
        //Deactivate
        leftLight.SetActive(false);
        rightLight.SetActive(false);
        midleLight.SetActive(false);
        
        //Music
        PlayMusic(musicDeFond,true);
        
    }

    private void Update()
    {
        if (!isFlickering)
        {
            if (Random.Range(0f, 1f) < 0.001f) // 1% de chance par frame
            {
                StartCoroutine(FlickerCoroutine());
            }
        }
    }

    private IEnumerator FlickerCoroutine()
    {
        isFlickering = true;
        PlaySoundEffect(lightFlicker);
        
        int flickers = Random.Range(2, 20);
        for (int i = 0; i < flickers; i++)
        {
            officeLight.enabled = false;
            yield return new WaitForSeconds(Random.Range(0.05f, 0.3f));
            officeLight.enabled = true;
            yield return new WaitForSeconds(Random.Range(0.05f, 0.3f));
        }
        
        yield return new WaitForSeconds(Random.Range(3f, 8f));
        isFlickering = false;
    }
    
    public void PlaySoundEffect(AudioClip soundEffect)
    {
        if (soundEffectSource == null) return;
        if (soundEffectSource.loop)
        {
            soundEffectSource.loop = false;
        }
        soundEffectSource.clip = soundEffect;
        soundEffectSource.Play();
    }
    
    public void PlayMusic(AudioClip soundEffect,bool loop)
    {
        if (musiqueSource == null) return;
        
        musiqueSource.loop = loop;
        
        musiqueSource.clip = soundEffect;
        
        musiqueSource.Play();
    }
    
}