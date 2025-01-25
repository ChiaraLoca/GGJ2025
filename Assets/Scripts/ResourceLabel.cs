using TMPro;
using UnityEngine;

public class ResourceLabel : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void SetLabel(string val)
    {
        text.text = val;
    }

}
