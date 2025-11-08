using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicButton : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _clickClip;

    public void ClickSound()
    {
        _audioSource.PlayOneShot(_clickClip);
    }
}
