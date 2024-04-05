using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    public AudioBankScriptableObject audioBank;
    
    public AudioSource ambianceSource;
    public AudioSource musicSource;
    public AudioSource sfxSource;

    private void Awake()
    {
        instance = this;
        if (ambianceSource.clip != null)
        {
            ambianceSource.Play();
        }
        if (musicSource.clip != null)
        {
            musicSource.Play();
        }
    }

    public void PlayAmbiance(AudioClip clip)
    {
        ambianceSource.clip = clip;
        ambianceSource.Play();
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.Play();
    }
}
