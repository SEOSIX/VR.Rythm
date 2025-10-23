using System;
using UnityEngine;



public enum SoundType
{
    CORRECTRYTHM,
    ALLCORRECT
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{

    [SerializeField] private AudioClip[] soundList;
    private static SoundManager instance;
    private AudioSource audioSoure;
    
    [Header("Pitch Settings")]
    [SerializeField] private float basePitch = 1f;
    [SerializeField] private float pitchIncrease = 0.05f;
    [SerializeField] private float maxPitch = 2f;
    private float currentPitch;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSoure = GetComponent<AudioSource>();
        currentPitch = basePitch;
    }

    public static void PlaySound(SoundType sound, float volume = 1)
    {
        instance.audioSoure.pitch = instance.currentPitch;
        instance.audioSoure.PlayOneShot(instance.soundList[(int)sound], volume);
    }
    public static void IncreasePitch()
    {
        instance.currentPitch = Mathf.Min(instance.currentPitch + instance.pitchIncrease, instance.maxPitch);
    }
    public static void ResetPitch()
    {
        instance.currentPitch = instance.basePitch;
    }
}
