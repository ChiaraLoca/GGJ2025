using MailKit;
using MailKit.Net.Imap;
using MailKit.Security;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using UnityEngine;



namespace TestClient
{
    class MailController : MonoBehaviour
    {
        //Disperazione

        async void Start()
        {
            Debug.Log("Start of the method");

            await Main();

            Debug.Log("After the async operation");
        }

        static async Task Main()
        {
            await ReceiveEmailWithOAuth2();
        }

        static async Task ReceiveEmailWithOAuth2()
        {
            // Carica le credenziali OAuth2
            UserCredential credential;

            

            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    new[] { "https://www.googleapis.com/auth/gmail.readonly" },
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true));
            }

            // Ottieni il token di accesso
            string accessToken = credential.Token.AccessToken;

            // Connetti al server Gmail tramite IMAP
            using (var client = new ImapClient())
            {
                try
                {
                    // Connetti al server IMAP di Gmail
                    await client.ConnectAsync("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);

                    // Autenticati con OAuth2
                    var oauth2 = new SaslMechanismOAuth2("tuoindirizzo@gmail.com", accessToken);
                    await client.AuthenticateAsync(oauth2);

                    // Seleziona la casella di posta ("INBOX")
                    await client.Inbox.OpenAsync(FolderAccess.ReadOnly);

                    Console.WriteLine($"Numero totale di email: {client.Inbox.Count}");
                    Console.WriteLine($"Numero di email non lette: {client.Inbox.Unread}");

                    // Leggi le ultime 10 email
                    for (int i = client.Inbox.Count - 1; i >= Math.Max(client.Inbox.Count - 10, 0); i--)
                    {
                        var message = await client.Inbox.GetMessageAsync(i);
                        Console.WriteLine($"Mittente: {message.From}");
                        Console.WriteLine($"Oggetto: {message.Subject}");
                        Console.WriteLine($"Data: {message.Date}");
                        Console.WriteLine($"Contenuto: {message.TextBody}");
                        Console.WriteLine("------------------------------");
                    }

                    // Disconnetti il client
                    await client.DisconnectAsync(true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Errore durante la lettura delle email: " + ex.Message);
                }
            }
        }
    }
}

