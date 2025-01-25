using TMPro;
using UnityEngine;

public class MessageContentPanel : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void Initialize(string value)
    {
        text.text = value;
    }

    public void OnAnimationEnded()
    {
        Destroy(gameObject);
    }
}