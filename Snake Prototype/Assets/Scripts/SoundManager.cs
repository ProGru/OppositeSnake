using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource mainAudio;

    public SoundSettings settings;

    private void Start()
    {
        mainAudio.clip = settings.defaultButtonClip;
    }

    public void Mute(bool value)
    {
        settings.isMuted = value;
        mainAudio.mute = value;
    }

    public void AdioVolume(float volume)
    {
        float audioVolume = volume >= 0 && volume <= 1 ? volume : 1;
        mainAudio.volume = audioVolume;
        settings.volume = audioVolume;
    }

    public void PlayButton(AudioClip clip = null)
    {
        if (clip != null)
        {
            if (mainAudio != null)
            {
                mainAudio.clip = clip;
                mainAudio.Play();
            }
        }
        else
        {
            if (mainAudio != null)
            {
                mainAudio.clip = settings.defaultButtonClip;
                mainAudio.Play();
            }
        }
    }
}
