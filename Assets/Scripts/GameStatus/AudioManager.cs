using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioSource musicAudioSource;
    [SerializeField] AudioClip track1;
    [SerializeField] AudioClip track2;
    [SerializeField] AudioClip track3;
    [SerializeField] float fadeDuration = 1.0f;
    Coroutine coroutine;

    private void Awake()
    {
        instance = this;
    }

    public void track(int trackIndex, bool firstStart)
    {

        AudioClip nextTrack;
        switch (trackIndex)
        {
            case 1:
                nextTrack = track1;

                break;
            case 2:
                nextTrack = track2;
                break;
            case 3:
                nextTrack = track3;
                break;
            default:
                nextTrack = track1;
                break;
        }

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        coroutine = StartCoroutine(MusicChangeCoroutine(musicAudioSource, nextTrack, firstStart));


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
}
