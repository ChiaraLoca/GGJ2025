using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Collections.Generic;
using MimeKit;
using MailKit.Search;
using System;
using System.IO;

public static class MailController
    {
    
        public static List<MailModel> RetrieveEmail()
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
                    emails.Add(new MailModel
                    {
                        Subject = message.Subject,
                        Body = message.TextBody,
                        MailTo = message.To.ToString()
                    });
                    inbox.AddFlags(uid, MessageFlags.Seen, true);

                }
                client.Disconnect(true);
            }
            return emails;
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
