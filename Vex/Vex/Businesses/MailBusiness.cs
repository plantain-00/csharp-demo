using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

using MailKit.Net.Smtp;

using MimeKit;

using Vex.DbModels;

namespace Vex.Businesses
{
    public class MailBusiness : BaseBusiness
    {
        private static readonly string smtpServerAddress = ConfigurationManager.AppSettings["smtp_server_address"];
        private static readonly string email = ConfigurationManager.AppSettings["email"];
        private static readonly string emailPassword = ConfigurationManager.AppSettings["email_password"];

        public async Task NeedApproval()
        {
            var user = Account.CurrentUser;
            var hrs = Account.GetHrs();
            var admins = Account.GetAdmins();

            var emails = new List<string>();
            emails.AddRange(hrs.Select(h => h.Email));
            emails.AddRange(admins.Select(a => a.Email));

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(new string(email.TakeWhile(m => m != '@').ToArray()), email));
            foreach (var e in emails)
            {
                message.To.Add(new MailboxAddress(new string(e.TakeWhile(m => m != '@').ToArray()), e));
            }
            message.Cc.Add(new MailboxAddress(new string(user.Email.TakeWhile(m => m != '@').ToArray()), user.Email));
            message.Subject = string.Format("-	EmailTitle: [PCI Union Intranet]User: {0}'s membership need your approval", user.GetName());

            message.Body = new TextPart("plain")
                           {
                               Text = message.Subject
                           };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(smtpServerAddress);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(new string(email.TakeWhile(m => m != '@').ToArray()), emailPassword);

                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }

        public async Task Approved(User user)
        {
            var hrs = Account.GetHrs();
            var admins = Account.GetAdmins();

            var emails = new List<string>();
            emails.AddRange(hrs.Select(h => h.Email));
            emails.AddRange(admins.Select(a => a.Email));

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(new string(email.TakeWhile(m => m != '@').ToArray()), email));
            foreach (var e in emails)
            {
                message.Cc.Add(new MailboxAddress(new string(e.TakeWhile(m => m != '@').ToArray()), e));
            }
            message.To.Add(new MailboxAddress(new string(user.Email.TakeWhile(m => m != '@').ToArray()), user.Email));
            message.Subject = string.Format("[PCI Union Intranet] {0}: Your request of Union membership has been approved", user.GetName());

            message.Body = new TextPart("plain")
                           {
                               Text = message.Subject
                           };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(smtpServerAddress);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(new string(email.TakeWhile(m => m != '@').ToArray()), emailPassword);

                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }

        public async Task Rejected(User user)
        {
            var hrs = Account.GetHrs();
            var admins = Account.GetAdmins();

            var emails = new List<string>();
            emails.AddRange(hrs.Select(h => h.Email));
            emails.AddRange(admins.Select(a => a.Email));

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(new string(email.TakeWhile(m => m != '@').ToArray()), email));
            foreach (var e in emails)
            {
                message.Cc.Add(new MailboxAddress(new string(e.TakeWhile(m => m != '@').ToArray()), e));
            }
            message.To.Add(new MailboxAddress(new string(user.Email.TakeWhile(m => m != '@').ToArray()), user.Email));
            message.Subject = string.Format("[PCI Union Intranet] {0}: Your request of Union membership has been rejected", user.GetName());

            message.Body = new TextPart("plain")
                           {
                               Text = message.Subject
                           };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(smtpServerAddress);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(new string(email.TakeWhile(m => m != '@').ToArray()), emailPassword);

                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }

        public async Task InformStatusChanged(User user)
        {
            var hrs = Account.GetHrs();
            var admins = Account.GetAdmins();

            var emails = new List<string>
                         {
                             user.Email
                         };
            emails.AddRange(hrs.Select(h => h.Email));
            emails.AddRange(admins.Select(a => a.Email));

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(new string(email.TakeWhile(m => m != '@').ToArray()), email));
            foreach (var e in emails)
            {
                message.To.Add(new MailboxAddress(new string(e.TakeWhile(m => m != '@').ToArray()), e));
            }
            message.Subject = string.Format("[PCI website]User: {0}’s membership status is changed to \"{1}\"", user.GetName(), user.Status);

            message.Body = new TextPart("plain")
                           {
                               Text = message.Subject
                           };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(smtpServerAddress);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(new string(email.TakeWhile(m => m != '@').ToArray()), emailPassword);

                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}