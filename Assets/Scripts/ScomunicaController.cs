using UnityEngine;
using Utility.GameEventManager;

public class ScomunicaController : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.AddListener<ScomunicaEvent>(OnScomunica);
    }

    private void OnScomunica(ScomunicaEvent evt)
    {
        evt.player.Scomunica = true;

        MailController.SendEmailAsync(evt.player.Mail, "SCOMUNICA", MessageHelper.GetMailTextScomunica(evt.player.Name));
        EventManager.Broadcast(new AddMessageEvent("<color=#ff0000> <b>Il cantone " + evt.player.Name + " è stato scomunicato</b> </color>" ));
    }
}
