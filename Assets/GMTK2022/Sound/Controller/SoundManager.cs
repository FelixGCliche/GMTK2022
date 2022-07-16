using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonPersistant<SoundManager>
{
    private const int AUDIO_SOURCE_AMOUNT = 10;

    private AudioSource[] audioSource;

    private int currentAudioSource = 0;

    public void InitAudioSource() 
    {
        audioSource = new AudioSource[AUDIO_SOURCE_AMOUNT];
        for(int i = 0; i < AUDIO_SOURCE_AMOUNT; i++)
        {
            GameObject newAudioSource = new GameObject();
            newAudioSource.name = "AudioSource" + i;
            audioSource[i] = newAudioSource.AddComponent<AudioSource>();
        }
    }

    public void PlaySfx(AudioClip sfx, Vector3 sfxPosition, bool randomizePitch = false)
    {
        audioSource[currentAudioSource].clip = sfx;
        audioSource[currentAudioSource].transform.position = sfxPosition;

        if (randomizePitch)
        {
            audioSource[currentAudioSource].pitch = Random.Range(0.5f, 1.5f);
        }
        else
        {
            audioSource[currentAudioSource].pitch = 1;
        }

        audioSource[currentAudioSource].Play();

        if (++currentAudioSource >= AUDIO_SOURCE_AMOUNT)
            currentAudioSource = 0;
    }
}
