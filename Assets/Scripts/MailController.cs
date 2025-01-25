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


public static class MailController
    {

    /*public static List<MailModel> RetrieveEmail()
    {
        List<MailModel> emails = new List<MailModel>();
        using (var client = new ImapClient())
        {
            client.Connect("imap.gmail.com", 993, true);
            client.Authenticate("papa.sisto.quarto@gmail.com", "zpac evik xmbc cdry");
            var inbox = client.Inbox;

            inbox.Open(FolderAccess.ReadWrite);

            // Retrieve only unread messages
            var uids = inbox.Search(SearchQuery.NotSeen);
            foreach (var uid in uids)
            {
                var message = inbox.GetMessage(uid);
                string pattern = @"<([^>]+)>";

                Match match = Regex.Match(message.From.ToString(), pattern);




            emails.Add(new MailModel
            {
                Subject = message.Subject,
                Body = message.TextBody,
                MailFrom = match.Groups[1].Value
            });
                inbox.AddFlags(uid, MessageFlags.Seen, true);

            }
            client.Disconnect(true);
        }
        return emails;
    }*/
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

    public static IEnumerator RunRetrieveEmailAsyncAsCoroutine()
    {
        // Esegui il metodo Task e attendi il suo completamento
        yield return TaskToCoroutine(RetrieveEmailAsync());

        // Codice da eseguire dopo il completamento del Task
        Debug.Log("Task completato!");
    }

    public static List<MailModel> retrievedEmailList;

    public static List<MailModel> GetRetrievedEmailList()
    { 
        return retrievedEmailList;
    }
    public static async Task RetrieveEmailAsync()
    {
        retrievedEmailList = new List<MailModel>();

        // Eseguiamo l'operazione in un task separato per evitare il blocco del thread principale
        await Task.Run(() =>
        {
            using (var client = new ImapClient())
            {
                client.Connect("imap.gmail.com", 993, true);
                client.Authenticate("papa.sisto.quarto@gmail.com", "zpac evik xmbc cdry");
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

                client.Disconnect(true);
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


  
    }
