using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : GenericSingleton<SoundManager>
{
    [SerializeField] private AudioSource _mainAudioSource, _battleAudioSource;
    public AudioSource _openGateAudioSource;

    public void PlayMainSound(AudioClip clip)
    {
        _mainAudioSource.PlayOneShot(clip);
    }
    
    public void PlayBattleSound(AudioClip clip)
    {
        _battleAudioSource.PlayOneShot(clip);
    }
    
    public void PlayOpenGateSound(AudioClip clip)
    {
        _openGateAudioSource.PlayOneShot(clip);
    }

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }
    
    public void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            _mainAudioSource.Play();
            _battleAudioSource.Stop();
        }
        else if (level == 1)
        {
            _battleAudioSource.Play();
            _mainAudioSource.Stop();
        }
    }
}
