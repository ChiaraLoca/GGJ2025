using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Collections.Generic;
using MimeKit;


    public static class MailController
    {
        private static ImapClient _imapClient;
        private static MailKit.Net.Smtp.SmtpClient _smtpClient;
        public static void Authenticate()
        {
            using (var client = new ImapClient())
            {
                client.Connect("imap.gmail.com", 993, true);

                client.Authenticate("papa.sisto.quarto@gmail.com", "zpac evik xmbc cdry");

                _imapClient = client;
            }
            

        }
        public static List<MailModel> RetrieveEmail()
        {
            List<MailModel> emails = new List<MailModel>();
            var inbox = _imapClient.Inbox;
            inbox.Open(FolderAccess.ReadOnly);
            //get only messages not opened
            for (int i = 0; i < inbox.Count; i++)
            {
                var message = inbox.GetMessage(i);
                emails.Add(new MailModel
                {
                    Subject = message.Subject,
                    Body = message.TextBody,
                    MailTo = message.To.ToString()
                });
                //move the message in the folder "opened"
                inbox.MoveTo(i, inbox.GetSubfolder("Opened"));
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
