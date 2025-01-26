using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utility.GameEventManager;

public class BollaPanel : MonoBehaviour
{
    private BollaModel _bolla;
    public TextMeshProUGUI text;

    public void Initialize(BollaModel bolla)
    {
        _bolla = bolla;
        text.text = bolla.getDescription();   
    }

    public void OnAnimationEnded()
    {
        EventManager.Broadcast(new AddBollaToBoardEvent(_bolla));

        Destroy(this.gameObject);
    }
}
