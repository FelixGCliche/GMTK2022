using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonPersistant<SoundManager>
{
    private const int AUDIO_SOURCE_AMOUNT = 10;

    bool isInitialized = false;
    private AudioSource musicSource;
    private AudioSource[] sfxSources;

    private int currentAudioSource = 0;

    public void InitAudioSource() 
    {
        if (!isInitialized)
        {
            GameObject newAudioSource = new GameObject();
            DontDestroyOnLoad(newAudioSource.gameObject);
            newAudioSource.name = "MusicSource";
            musicSource = newAudioSource.AddComponent<AudioSource>();

            sfxSources = new AudioSource[AUDIO_SOURCE_AMOUNT];
            for(int i = 0; i < AUDIO_SOURCE_AMOUNT; i++)
            {
                newAudioSource = new GameObject();
                DontDestroyOnLoad(newAudioSource.gameObject);
                newAudioSource.name = "SFXSource" + i;
                sfxSources[i] = newAudioSource.AddComponent<AudioSource>();
            }
            isInitialized = true;
        }
    }

    public void PlayMusic(AudioClip music)
    {
        if(musicSource.clip != music)
        {
            musicSource.clip = music;
            musicSource.Play();
        }
    }

    public void PlaySfx(AudioClip sfx, Vector3 sfxPosition, bool randomizePitch = false)
    {
        sfxSources[currentAudioSource].clip = sfx;
        sfxSources[currentAudioSource].transform.position = sfxPosition;

        if (randomizePitch)
        {
            sfxSources[currentAudioSource].pitch = Random.Range(0.5f, 1.5f);
        }
        else
        {
            sfxSources[currentAudioSource].pitch = 1;
        }

        sfxSources[currentAudioSource].Play();

        if (++currentAudioSource >= AUDIO_SOURCE_AMOUNT)
            currentAudioSource = 0;
    }

    public void SetVolume(float value)
    {
        musicSource.volume = value;
        foreach (AudioSource source in sfxSources)
        {
            source.volume = value;
        }
    }
}
