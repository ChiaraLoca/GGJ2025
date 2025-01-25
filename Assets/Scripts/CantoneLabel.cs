using TMPro;
using UnityEngine;

public class CantoneLabel : MonoBehaviour
{
    public TextMeshProUGUI text;


    public void SetLabel(string val)
    {
        text.text = val;
    }


}