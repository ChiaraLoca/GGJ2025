using GameStatus;
using System.Collections.Generic;
using UnityEngine;
using Utility.GameEventManager;

public class ResourceLabelsPanel : MonoBehaviour
{
    public ResourceLabel resourceLabelPrefab;
    public Transform labelsParent;

    private void Awake()
    {
        EventManager.AddListener<EndGameEvent>(OnEnd);
    }

    public void Initialize(List<string> strings)
    {
        foreach (string s in strings)
        {
            ResourceLabel label = Instantiate(resourceLabelPrefab, labelsParent);
            label.SetLabel(s);
        }
    }

    private void OnEnd(EndGameEvent @event)
    {
        Destroy(this.gameObject);
    }
}
