using System;
using System.Collections.Generic;
using UnityEngine;
using Utility.GameEventManager;

public class CantoneLabelsPanel : MonoBehaviour
{
    public CantoneLabel cantoneLabelPrefab;
    public Transform labelsParent;

    private List<CantoneLabel> _cantoneLabels = new List<CantoneLabel>();

    private void OnEnable()
    {
        EventManager.AddListener<AddNewCantone>(OnAddNewCantone);
    }

    private void OnAddNewCantone(AddNewCantone cantone)
    {
        
    }

    public void Initialize(List<string> strings)
    {
        foreach (string s in strings)
        {
            CantoneLabel label = Instantiate(cantoneLabelPrefab, labelsParent);
            _cantoneLabels.Add(label);
            label.SetLabel(s);
        }
    }
}

public class AddNewCantone : IGameEvent
{ 

}