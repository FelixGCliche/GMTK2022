using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] grabSfx;
    [SerializeField] private AudioSource[] dropSfx;

    public void OnGrab()
    {
        grabSfx[Random.Range(0,grabSfx.Length)].Play();
    }

    public void OnDrop()
    {
        dropSfx[Random.Range(0,dropSfx.Length)].Play();
    }
}
