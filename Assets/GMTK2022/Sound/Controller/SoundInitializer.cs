using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundInitializer : MonoBehaviour
{
    [SerializeField] private AudioClip sceneMusic;

    // Start is called before the first frame update
    private void Start()
    {
        SoundManager.Instance.InitAudioSource();
        SoundManager.Instance.PlayMusic(sceneMusic);
    }
}
