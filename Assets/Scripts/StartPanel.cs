using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using Utility.GameEventManager;

public class StartPanel : MonoBehaviour
{
    public Button _button;
    
    private void OnEnable()
    {
        _button.onClick.AddListener(Button);
    }
    private void OnDisable()
    {
        _button.onClick.RemoveListener(Button);
    }

    public void Button()
    {
        
            EventManager.Broadcast(new StartEvent());
            gameObject.SetActive(false);
            Destroy(gameObject);

        
    }
}



public class StartEvent : IGameEvent
{ }
