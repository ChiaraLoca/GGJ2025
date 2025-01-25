using UnityEngine;
using Utility.GameEventManager;

public class PopeController : MonoBehaviour
{
    
    public void Initialize()
    {
       EventManager.Broadcast(new ShowMessageEvent("hic sunt papam!\npapa.sisto.quarto@gmail.com"));
    }


    
}