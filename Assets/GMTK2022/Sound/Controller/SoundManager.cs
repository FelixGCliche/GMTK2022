using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
            musicSource.loop = true;

            sfxSources = new AudioSource[AUDIO_SOURCE_AMOUNT];
            for(int i = 0; i < AUDIO_SOURCE_AMOUNT; i++)
            {
                newAudioSource = new GameObject();
                DontDestroyOnLoad(newAudioSource.gameObject);
                newAudioSource.name = "SFXSource" + i;
                sfxSources[i] = newAudioSource.AddComponent<AudioSource>();
                sfxSources[i].spatialBlend = 0.5f;
            }
            isInitialized = true;
        }
    }

    public void PlayMusic(AudioClip music)
    {
        if(musicSource.clip != music)
        {
            StartCoroutine(FadeInMusic(music, musicSource.volume));
        }
    }

    private IEnumerator FadeInMusic(AudioClip music, float targetVolume)
    {
        musicSource.DOFade(0f, 1f);
        yield return new WaitForSeconds(1.1f);
        musicSource.clip = music;
        musicSource.Play();
        musicSource.DOFade(targetVolume, 0.5f);
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

    public void SetMusicVolume(float value)
    {
        musicSource.volume = value;
    }

    public void SetSoundVolume(float value)
    {
        foreach (var audioSource in sfxSources)
            audioSource.volume = value;
    }
}
