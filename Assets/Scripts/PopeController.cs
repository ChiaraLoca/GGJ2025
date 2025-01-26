using System;
using UnityEngine;
using Utility.GameEventManager;

public class PopeController : MonoBehaviour
{
    public Animator _animator;

    private void Awake()
    {
        EventManager.AddListener<SendResponseEvent>(OnResponseSent);
        EventManager.AddListener<ScomunicaEvent>(OnScomunica);

    }

    private void OnScomunica(ScomunicaEvent evt)
    {
        _animator.SetTrigger("scomunica");
    }

    private void OnResponseSent(SendResponseEvent evt)
    {
        _animator.SetTrigger("bubble");
    }

    public void Initialize()
    {
       EventManager.Broadcast(new AddMessageEvent("hic sunt papam!\npapa.sisto.quarto@gmail.com"));
    }


    
}
