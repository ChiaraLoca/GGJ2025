using GameStatus;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.GameEventManager;

public class BollaBoardPanel : MonoBehaviour
{
    public BollaBoardLabel _boardLabelPrefab;
    public Transform transformParent;

    private void Awake()
    {
        EventManager.AddListener<AddBollaToBoardEvent>(OnAddBollaToBoard);
        EventManager.AddListener<EndGameEvent>(OnEnd);
    }

    private void OnEnd(EndGameEvent evt)
    {
        Destroy(gameObject);
    }

    public void OnAddBollaToBoard(AddBollaToBoardEvent evt)
    {
        BollaBoardLabel bollaBoardLabel = Instantiate(_boardLabelPrefab, transformParent);
        bollaBoardLabel.Initialize(evt.BollaModel);
    }

    
}
public class AddBollaToBoardEvent : IGameEvent
{
    public BollaModel BollaModel;

    public AddBollaToBoardEvent(BollaModel bollaModel)
    {
        BollaModel = bollaModel;
    }
}
