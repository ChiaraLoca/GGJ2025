using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Collections.Generic;
using MimeKit;
using MailKit.Search;
using System.Text.RegularExpressions;
using System;




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

                message.Body = new TextPart("html")
                {
                    Text = body
                };
                client.Send(message);
                client.Disconnect(true);
            }
     
        }


  
    }
