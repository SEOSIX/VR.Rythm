using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class OfficeEvent : MonoBehaviour
{

    public SceneChanger instance;
    
    [Header("Lights part")]
    public Light officeLight;
    public GameObject leftLight;
    public GameObject rightLight;
    public GameObject midleLight;
    
    [Header("Jumpscare part")]
    public GameObject pictureInWorld;
    public bool animInPlay;
    public bool jumpscareActive;
    public Animator anim;

    [Header("Sound Source")] 
    public AudioSource musiqueSource;
    public AudioSource soundEffectSource;

    [Header("Sound Clip")] 
    public AudioClip musicDeFond;
    public AudioClip lightFlicker;
    
    
    private bool isFlickering = false;

    private void Start()
    {

        RenderSettings.fogDensity = 0.300f;
        
        //Light
        leftLight.SetActive(false);
        rightLight.SetActive(false);
        midleLight.SetActive(false);
        
        //Music
        PlayMusic(musicDeFond,true);
        
        //Jumpscare
        pictureInWorld.SetActive(false);
        animInPlay = false;
        
    }

    private void Update()
    {
        if (!isFlickering)
        {
            if (Random.Range(0f, 1f) < 0.001f) // 1% de chance par frame
            {
                StartCoroutine(FlickerCoroutine(Random.Range(2, 20)));
            }
        }
        
        if (Input.GetKeyDown(KeyCode.KeypadEnter) && !animInPlay)
        {
            StartCoroutine(JumpscareAnimCorout());
        }
    }

    private IEnumerator FlickerCoroutine(int flickers)
    {
        isFlickering = true;
        PlaySoundEffect(lightFlicker);
        
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
    
    public IEnumerator JumpscareAnimCorout()
    {
        jumpscareActive = true;
        
        pictureInWorld.SetActive(true);
        
        yield return StartCoroutine(FlickerCoroutine(20));
        officeLight.enabled = false;
        pictureInWorld.SetActive(false);
        
        yield return new WaitForSeconds(Random.Range(2f, 8f));
        
        animInPlay = false;
        anim.SetTrigger("Jumpscare");
        
        yield return new WaitForSeconds(5.30f);
        
        instance.LoadDeathMenu();
        
        yield return new WaitForSeconds(20f);
        
        animInPlay = false;
    }
    
    public void PlayMusic(AudioClip soundEffect,bool loop)
    {
        if (musiqueSource == null) return;
        
        musiqueSource.loop = loop;
        
        musiqueSource.clip = soundEffect;
        
        musiqueSource.Play();
    }
    
}