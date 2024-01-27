using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TouchToStart.Utility;
using TouchToStart.Sound;
//using TouchToStart.Utility;

namespace TouchToStart.Sound
{
    public enum SoundType
    {
        success = 0,
        fail = 1,
        press = 2,
        release = 3,

        edgedenied = 4,
    }
}


[RequireComponent(typeof(AudioSource))]
public class AudioEvents : Singleton<AudioEvents>
{
    [SerializeReference]
    public AudioClip[] sounds;
    
    AudioSource audioSource;

    [SerializeField]
    public float defaultVolume = 0.8f;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }
    public void PlaySound(SoundType soundtype)
    {
        audioSource.PlayOneShot(sounds[(int)soundtype], defaultVolume);
    }
}
