using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BollaBoardLabel : MonoBehaviour
{
    public TextMeshProUGUI text;

    internal void Initialize(BollaModel bollaModel)
    {
        text.text = bollaModel.getShortDescription();
    }

    
}
