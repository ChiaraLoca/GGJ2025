using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;

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