using TMPro;
using UnityEngine;

public class AddCantoneLabel : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void Initialize(string value,float time)
    { 
        text.text = value;
        Destroy(gameObject, time);
    }
}

