using MailKit;
using MailKit.Net.Imap;
using MailKit.Security;
using System.Collections.Generic;
using MimeKit;
using MailKit.Search;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;

public class MailController : MonoBehaviour
{
    private ImapClient client;
    public static List<MailModel> retrievedEmailList = new List<MailModel>();

    void Start()
    {
        // Avvia la coroutine per scaricare le email in tempo reale
        StartCoroutine(CheckEmailsPeriodically());
    }

    IEnumerator CheckEmailsPeriodically()
    {
        // Connetti e autentica una volta
        yield return TaskToCoroutine(ConnectAndAuthenticateAsync());

        while (true)
        {
            // Esegui il metodo per ricevere le email
            yield return TaskToCoroutine(RetrieveEmailAsync());

            // Attendi 2 secondi prima di eseguire nuovamente
            yield return new WaitForSeconds(2);
        }
    }

    public static IEnumerator TaskToCoroutine(Task task)
    {
        while (!task.IsCompleted)
        {
            yield return null;
        }

        if (task.IsFaulted)
        {
            throw task.Exception;
        }
    }

    private async Task ConnectAndAuthenticateAsync()
    {
        client = new ImapClient();
        await client.ConnectAsync("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
        await client.AuthenticateAsync("papa.sisto.quarto@gmail.com", "zpac evik xmbc cdry");
    }

    public static List<MailModel> GetRetrievedEmailList()
    {
        return retrievedEmailList;
    }

    public async Task RetrieveEmailAsync()
    {
        retrievedEmailList = new List<MailModel>();

        // Eseguiamo l'operazione in un task separato per evitare il blocco del thread principale
        await Task.Run(() =>
        {
            var inbox = client.Inbox;
            inbox.Open(FolderAccess.ReadWrite);

            // Recupera solo i messaggi non letti
            var uids = inbox.Search(SearchQuery.NotSeen);
            foreach (var uid in uids)
            {
                var message = inbox.GetMessage(uid);
                string pattern = @"<([^>]+)>";
                Match match = Regex.Match(message.From.ToString(), pattern);

                retrievedEmailList.Add(new MailModel
                {
                    Subject = message.Subject,
                    Body = message.TextBody,
                    MailFrom = match.Groups[1].Value
                });

                // Segna il messaggio come visto
                inbox.AddFlags(uid, MessageFlags.Seen, true);
            }
        });
    }

    public static void SendEmail(string to, string subject, string body)
    {
        using (var client = new MailKit.Net.Smtp.SmtpClient())
        {
            client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            client.Authenticate("papa.sisto.quarto@gmail.com", "zpac evik xmbc cdry");
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Papa Sisto IV", "papa.sisto.quarto@gmail.com"));
            message.To.Add(new MailboxAddress("", to));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = body
            };
            var logoPath = "Assets/Images/sigillo3D.png"; // Path relativo o assoluto dell'immagine
            var logo = bodyBuilder.LinkedResources.Add(logoPath);
            logo.ContentId = "logo-image"; // Deve corrispondere al CID specificato nell'HTML
            logo.ContentDisposition = new ContentDisposition(ContentDisposition.Inline);
            logo.ContentType.MediaType = "image";
            logo.ContentType.MediaSubtype = "png";
            message.Body = bodyBuilder.ToMessageBody();
            client.Send(message);
            client.Disconnect(true);
        }
    }

    void OnDestroy()
    {
        // Disconnetti il client quando il GameObject viene distrutto
        if (client != null && client.IsConnected)
        {
            client.Disconnect(true);
            client.Dispose();
        }
    }
}