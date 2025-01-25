using System;
using UnityEngine;
using Utility.GameEventManager;

public class AddCantoneLabelsPanel : MonoBehaviour
{
    public AddCantoneLabel cantoneLabelPrefab;
    public Transform labelParent;
    
    private void OnEnable()
    {
        EventManager.AddListener<AddNewCantoneEvent>(OnAddNewCantone);
        EventManager.AddListener<StartEvent>(OnStart);
    }

    private void OnStart(StartEvent evt)
    {
        Destroy(gameObject);
        
    }

    private void OnAddNewCantone(AddNewCantoneEvent evt)
    {
        AddCantoneLabel addCantoneLabel= Instantiate(cantoneLabelPrefab, labelParent);
        addCantoneLabel.Initialize(evt.playerModel.Name, 5);
        addCantoneLabel.transform.SetAsFirstSibling();
    }

   
}

