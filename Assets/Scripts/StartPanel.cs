using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using Utility.GameEventManager;

public class StartPanel : MonoBehaviour
{
    public Button _button;
    
    private void Awake()
    {
        _button.onClick.AddListener(() => {
            
            EventManager.Broadcast(new StartEvent());
            Destroy(gameObject);
                });
    }

}

public class StartEvent : IGameEvent
{ }
