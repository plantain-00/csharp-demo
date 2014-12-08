#define Receive
using System;

using MailKit.Net.Pop3;
using MailKit.Net.Smtp;

using MimeKit;

namespace MailKit.Demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
#if Send
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("mailkit_test", "mailkit_test@163.com"));
            message.To.Add(new MailboxAddress("yaoyao12306", "yaoyao12306@163.com"));
            message.Subject = "How you doin'?";

            message.Body = new TextPart("plain")
                           {
                               Text = @"Hey Chandler,

I just wanted to let you know that Monica and I were going to go play some paintball, you in?

-- Joey"
                           };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.163.com");
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate("mailkit_test", "mailkit_password");

                client.Send(message);
                client.Disconnect(true);
            }
#endif

#if Receive
            using (var client = new Pop3Client())
            {
                client.Connect("pop.163.com", 110);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate("mailkit_test", "mailkit_password");

                var count = client.GetMessageCount();
                for (var i = 0; i < count; i++)
                {
                    var message = client.GetMessage(i);
                    Console.WriteLine("Subject: {0}", message.Subject);
                }

                client.Disconnect(true);
            }
#endif
        }
    }
}