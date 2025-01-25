using System;
using System.Collections.Generic;
using UnityEngine;
public class CantoneLabelsPanel : MonoBehaviour
{
    public CantoneLabel cantoneLabelPrefab;
    public Transform labelsParent;

    private List<CantoneLabel> _cantoneLabels = new List<CantoneLabel>();


    public void Initialize()
    {
        foreach (PlayerModel player in GameStatus.GameStatusManager.instance.Players)
        {
            CantoneLabel label = Instantiate(cantoneLabelPrefab, labelsParent);
            _cantoneLabels.Add(label);
            label.SetLabel(player.Name);
        }
    }
}

