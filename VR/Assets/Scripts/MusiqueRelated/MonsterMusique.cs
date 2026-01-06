using System;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMusique : MonoBehaviour
{
    
    // "Custom" trigger https://www.youtube.com/watch?v=xzg42EJ2BHA
    [Header("Trigger")]
    public TriggerMonsterMusique FarTrigger;
    public TriggerMonsterMusique ClosingTrigger;
    public TriggerMonsterMusique NearTrigger;
    public TriggerMonsterMusique AlmostThereTrigger;
    
    
    //Sound
    private MusicManager musiqueManager;
    
    [Header("Musique Source")]
    public AudioSource MonsterMusiqueSource;
    
    [Header("Musique Clip")]
    public AudioClip FarAudio;
    public AudioClip ClosingDistanceAudio;
    public AudioClip NearAudio;
    public AudioClip AlmostThereAudio;

    private void Awake()
    {
        FarTrigger.EnteredTrigger += OnFarTriggerEntered;
        ClosingTrigger.EnteredTrigger += OnClosingTriggerEntered;
        NearTrigger.EnteredTrigger += OnNearTriggerEntered;
        AlmostThereTrigger.EnteredTrigger += OnAlmostThereTriggerEntered;
    }

    private void Start()
    {
        musiqueManager.PlayMusic(MonsterMusiqueSource,FarAudio,true);
    }

    void OnFarTriggerEntered(Collider other)
    {
        musiqueManager.PlayMusic(MonsterMusiqueSource,FarAudio,true);
    }
    
    void OnFarTriggerExited(Collider other)
    {
        
    }
    
    void OnClosingTriggerEntered(Collider other)
    {
        musiqueManager.PlayMusic(MonsterMusiqueSource,ClosingDistanceAudio,true);
    }
    
    void OnClosingTriggerExited(Collider other)
    {
        
    }
    
    void OnNearTriggerEntered(Collider other)
    {
        musiqueManager.PlayMusic(MonsterMusiqueSource,NearAudio,true);
    }
    
    void OnNearTriggerExited(Collider other)
    {
        
    }
    
    void OnAlmostThereTriggerEntered(Collider other)
    {
        musiqueManager.PlayMusic(MonsterMusiqueSource,AlmostThereAudio,true);
    }
    
    void OnAlmostThereTriggerExited(Collider other)
    {
        
    }
    
    
}
