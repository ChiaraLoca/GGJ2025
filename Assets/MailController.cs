using MailKit;
using MailKit.Net.Imap;
using MailKit.Security;
using UnityEngine;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Net.Mail;

namespace TestClient
{
    class MailController : MonoBehaviour
    {
        //Disperazione

        void Start()
        {
            Debug.Log("Start of the method");

            // Avvia la coroutine per scaricare le email ogni 2 secondi
            StartCoroutine(CheckEmailsPeriodically());
        }

        IEnumerator CheckEmailsPeriodically()
        {
            while (true)
            {
                // Esegui il metodo per ricevere le email
                yield return ReceiveEmailWithOAuth2();

                // Attendi 2 secondi prima di eseguire nuovamente
                yield return new WaitForSeconds(2);
            }
        }

        static async Task ReceiveEmailWithOAuth2()
        {
            using (var client = new ImapClient())
            {
                client.Connect("imap.gmail.com", 993, true);

                client.Authenticate("papa.sisto.quarto@gmail.com", "Scomunica64!");

                // The Inbox folder is always available on all IMAP servers...
                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadOnly);

                Console.WriteLine("Total messages: {0}", inbox.Count);
                Console.WriteLine("Recent messages: {0}", inbox.Recent);

                for (int i = 0; i < inbox.Count; i++)
                {
                    var message = inbox.GetMessage(i);
                    Console.WriteLine("Subject: {0}", message.Subject);
                }

                client.Disconnect(true);
            }
        }
    }
}