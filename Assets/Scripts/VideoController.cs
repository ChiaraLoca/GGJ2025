using GameStatus;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using Utility.GameEventManager;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    private void Awake()
    {
        EventManager.AddListener<EndGameEvent>(OnEnd);
    }

    private void OnEnd(EndGameEvent @event)
    {
        videoPlayer.Pause();
    }

    private void Start()
    {
        videoPlayer.Prepare();
        StartCoroutine(WaitVideoReady());
    }

    public IEnumerator WaitVideoReady()
    { 
        yield return new WaitUntil(()=>videoPlayer.isPrepared);
        videoPlayer.Play();
    }
}