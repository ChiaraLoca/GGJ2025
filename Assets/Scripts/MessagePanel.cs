using TMPro;
using UnityEngine;

using Utility.GameEventManager;

public class MessagePanel : MonoBehaviour 
{
    public GameObject panel;
    public TextMeshProUGUI text;

    private void OnEnable()
    {
        EventManager.AddListener<ShowMessageEvent>(OnShowMessage);
        EventManager.AddListener<HideMessageEvent>(OnHideMessage);
    }

    public void OnShowMessage(ShowMessageEvent evt)
    {
        panel.gameObject.SetActive(true);
        text.text = evt.message;
    }

    public void OnHideMessage(HideMessageEvent evt)
    {
        panel.gameObject.SetActive(false);
        panel.gameObject.SetActive(false);
    }
}

public class ShowMessageEvent : IGameEvent
{
    public string message;

    public ShowMessageEvent(string message)
    {
        this.message = message;
    }
}
public class HideMessageEvent : IGameEvent
{
    
}