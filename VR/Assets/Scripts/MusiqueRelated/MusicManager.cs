using UnityEngine;

public class MusicManager : MonoBehaviour
{
    
    [Header("Sound Source")] 
    public AudioSource soundEffectSource;
    
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
    
    public void PlayMusic(AudioSource audioSource,AudioClip soundEffect,bool loop)
    {
        if (audioSource == null) return;
        
        audioSource.loop = loop;
        
        audioSource.clip = soundEffect;
        
        audioSource.Play();
    }
    
    
}
