using System;
using System.Collections.Generic;
using UnityEngine;
using Utility.GameEventManager;
public class CantoneLabelsPanel : MonoBehaviour
{
    public CantoneLabel cantoneLabelPrefab;
    public Transform labelsParent;

    private List<CantoneLabel> _cantoneLabels = new List<CantoneLabel>();

    private void Awake()
    {
        EventManager.AddListener<ScomunicaEvent>(OnScomunica);
    }

    private void OnScomunica(ScomunicaEvent evt)
    {
        foreach (Transform child in labelsParent)
        {

            CantoneLabel cantoneLabel = child.GetComponent<CantoneLabel>();
            if (cantoneLabel.GetPlayer().Equals(evt.player))
            {
                cantoneLabel.Scomunica();
            }
        }
    }

    public void Initialize()
    {
        foreach (PlayerModel player in GameStatus.GameStatusManager.instance.Players)
        {
            CantoneLabel label = Instantiate(cantoneLabelPrefab, labelsParent);
            label.Initialize(player);
            _cantoneLabels.Add(label);
            label.SetLabel(player.Name);
        }
    }
}

