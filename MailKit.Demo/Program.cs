using MailKit.Net.Smtp;

using MimeKit;

namespace MailKit.Demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
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
        }
    }
}