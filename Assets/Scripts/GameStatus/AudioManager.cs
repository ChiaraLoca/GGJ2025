using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Utility.GameEventManager;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioSource musicAudioSource;
    [SerializeField] AudioClip trackMenu;
    [SerializeField] AudioClip trackLevel;
    [SerializeField] AudioClip trackTimbro;
    [SerializeField] AudioClip trackPergamena;
    [SerializeField] AudioClip trackCampane;
    [SerializeField] AudioSource clipAudioSource;
    [SerializeField] float fadeDuration = 1.0f;
    Coroutine coroutine;

    private void Awake()
    {
        instance = this;
        EventManager.AddListener<StartEvent>(OnStart);
        EventManager.AddListener<AddNewCantoneEvent>(AddPlayer);
        EventManager.AddListener<SendResponseEvent>(OnResponseSent);
        EventManager.AddListener<CreateBubbleEvent>(OnBubbleCreate);
        StartCoroutine(Track(1, true));
    }

    private void OnStart(StartEvent @event)
    {
        StartCoroutine(Track(2, false));
    }

    private void AddPlayer(AddNewCantoneEvent @event)
    {
        StartCoroutine(Clip("pergamena"));
    }

    private void OnResponseSent(SendResponseEvent @event)
    {
        StartCoroutine(Clip("timbro"));
    }

    private void OnBubbleCreate(CreateBubbleEvent @event)
    {
        StartCoroutine(Clip("campane"));
    }



    public IEnumerator Clip(string clip)
    {
        AudioClip nextClip = null;
        switch (clip)
        {
            case "timbro":
                nextClip = trackTimbro;
                break;
            case "pergamena":
                nextClip = trackPergamena;
                break;
            case "campane":
                nextClip = trackCampane;
                break;
            default:
                
                break;
           ;
        }

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        if(nextClip!=null)
        coroutine = StartCoroutine(ClipChangeCoroutine(clipAudioSource, nextClip));

        yield return null;
    }

    public IEnumerator Track(int trackIndex, bool firstStart)
    {

        AudioClip nextTrack;
        switch (trackIndex)
        {
            case 1:
                nextTrack = trackMenu;

                break;
            case 2:
                nextTrack = trackLevel;
                break;
         
            default:
                nextTrack = trackMenu;
                break;
        }

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        coroutine = StartCoroutine(MusicChangeCoroutine(musicAudioSource, nextTrack, firstStart));

        yield return null;

    }
    private IEnumerator MusicChangeCoroutine(AudioSource musicAudioSource, AudioClip newTrack, bool firstStart)
    {

        float fadeDurationNew = firstStart ? 0f : fadeDuration;
        if (musicAudioSource.isPlaying)
        {
            // Fade out the current track
            float startVolume = musicAudioSource.volume;
            for (float t = 0; t < fadeDurationNew; t += Time.deltaTime)
            {
                musicAudioSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDurationNew);
                yield return null;
            }
            musicAudioSource.volume = 0;
            musicAudioSource.Stop();
        }

        // Change the track
        musicAudioSource.clip = newTrack;
        musicAudioSource.Play();

        // Fade in the new track
        for (float t = 0; t < fadeDurationNew; t += Time.deltaTime)
        {
            musicAudioSource.volume = Mathf.Lerp(0, 1, t / fadeDurationNew);
            yield return null;
        }
        musicAudioSource.volume = 1;
    }

     private IEnumerator ClipChangeCoroutine(AudioSource musicAudioSource, AudioClip newTrack)
    {

        if (musicAudioSource.isPlaying)
        {
            musicAudioSource.Stop();
        }
        musicAudioSource.clip = newTrack;
        musicAudioSource.Play();
        yield return null;

    }
}
