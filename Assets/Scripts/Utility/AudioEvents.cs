using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using TouchToStart.Utility;

[RequireComponent(typeof(AudioSource))]
public class AudioEvents : MonoBehaviour
{
    [SerializeField]
    private AudioClip soundSuccess, soundFail, soundOnbutton, soundReleasebutton;    
    
    AudioSource audioSource;
    private float defaultVolume = 0.8f;


    void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }
    public void PlaySuccess() {
        audioSource.PlayOneShot(soundSuccess, defaultVolume);
    }

    public void PlayFail() {
        audioSource.PlayOneShot(soundFail, defaultVolume);
    }

    public void PlayPressButton() {
        audioSource.PlayOneShot(soundOnbutton, defaultVolume);
    }

    public void PlayReleaseButton() {
        audioSource.PlayOneShot(soundReleasebutton, defaultVolume);
    }
}
