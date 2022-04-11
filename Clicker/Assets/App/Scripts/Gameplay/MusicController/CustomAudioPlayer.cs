using App.Scripts.UiControllers.GameScreen.MinersPanel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CustomAudioPlayer : MonoBehaviour
{
    [SerializeField] private MinerActiveSlotsEventsUiController activeSlot;

    [SerializeField] private AudioClipsContainier backgroundMusic;
    [SerializeField] private AudioSource backgroundAudioSource;
    [SerializeField] private AudioSource clickAudioSource;
    [SerializeField] private AudioSource buyAudioSource;
    [SerializeField] private AudioSource sellAudioSource;
    [SerializeField] private AudioSource levelUpAudioSource;
    [SerializeField] private AudioSource placeUpAudioSource;
    int trackNumber = 0;

    private void Start()
    {
        ChangeTrack();
        activeSlot.OnGirlPlaced += PlayPlaceSound;
    }
    
    //todo: Сделать более лучше, пожалуйста, это плохо. Но работает. Воспроизводит звук клика при нажатии ЛКМ везде
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlaySound(clickAudioSource);

        }
    }
   
    private void ChangeTrack()
    {
        Invoke("NextTrack", backgroundAudioSource.clip.length);
    }

    private void NextTrack()
    {

        trackNumber++;
        if (trackNumber == backgroundMusic.audioClips.Count)
        {
            trackNumber = 0;
        }
        backgroundAudioSource.clip = backgroundMusic.audioClips[trackNumber];
        backgroundAudioSource.PlayDelayed(1f);
        Invoke("NextTrack", backgroundAudioSource.clip.length);
    }

    private void PlaySound(AudioSource audioSource)
    {
        if (!clickAudioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
    
    public void PlayBuySound()
    {
        PlaySound(buyAudioSource);
    }

    public void PlaySellSound()
    {
        PlaySound(sellAudioSource);
    }

    public void PlayLevelUpSound()
    {
        levelUpAudioSource.Play();
    }

    public void PlayPlaceSound()
    {
        PlaySound(placeUpAudioSource);
    }
}
