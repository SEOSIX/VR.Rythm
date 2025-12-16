using UnityEngine;

public class MusicManager : MonoBehaviour
{
    
    [Header("Sound Source")] 
    public AudioSource musiqueSource;
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
    
    public void PlayMusic(AudioClip soundEffect,bool loop)
    {
        if (musiqueSource == null) return;
        
        musiqueSource.loop = loop;
        
        musiqueSource.clip = soundEffect;
        
        musiqueSource.Play();
    }
    
    
}
