using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioClipsContainier backgroundMusic;
    [SerializeField] private AudioSource audioSource;
    int trackNumber = 0;

    private void Start()
    {
        ChangeTrack();
    }

    void ChangeTrack()
    {
        Invoke("NextTrack", audioSource.clip.length);
    }

    void NextTrack()
    {

        trackNumber++;
        if (trackNumber == backgroundMusic.audioClips.Count)
        {
            trackNumber = 0;
        }
        audioSource.clip = backgroundMusic.audioClips[trackNumber];
        audioSource.Play();        
        Invoke("NextTrack", audioSource.clip.length);
    }
}
