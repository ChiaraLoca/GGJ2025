using GameStatus;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;

using Utility.GameEventManager;


public class MessagePanel : MonoBehaviour 
{
    public MessageContentPanel messageContentPanelPrefab;

    public Transform messageParent;

    public Queue<string> messages = new Queue<string>();

    private void Start()
    {
        EventManager.AddListener<AddMessageEvent>(OnAddMessage);
        EventManager.AddListener<EndGameEvent>(OnEnd);

        InvokeRepeating("ShowMessage", 5, 15);
    }

    public void OnAddMessage(AddMessageEvent evt)
    {
        messages.Enqueue(evt.message);

        
    }

    public void ShowMessage()
    {
        if (messages.Count > 0)
        {
            MessageContentPanel messageContentPanel = Instantiate(messageContentPanelPrefab, messageParent);
            messageContentPanel.Initialize(messages.Dequeue());
            messageContentPanel.transform.SetAsFirstSibling();
        }
    }

    private void OnEnd(EndGameEvent @event)
    {
        Destroy(this.gameObject);
    }

}

public class AddMessageEvent : IGameEvent
{
    public string message;

    public AddMessageEvent(string message)
    {
        this.message = message;
    }
}

