using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CantoneLabel : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Image image;

    public Color disabledColor;
    private PlayerModel _playerModel;

    public void SetLabel(string val)
    {
        text.text = val;
    }

    internal object GetPlayer()
    {
        return _playerModel;
    }

    internal void Initialize(PlayerModel player)
    {
        _playerModel = player;
    }

    internal void Scomunica()
    {
        image.color = disabledColor;
    }

    internal void Win(bool good)
    {
        if (!good)
            Destroy(gameObject);
    }
}