using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BollaPanel : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void Initialize(string val)
    {
        text.text = val;   
    }

    public void OnAnimationEnded()
    {
        Destroy(this.gameObject);
    }
}
