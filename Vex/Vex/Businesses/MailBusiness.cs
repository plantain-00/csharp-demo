using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

using MimeKit;

using Vex.DbModels;

using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Vex.Businesses
{
    public class MailBusiness : BaseBusiness
    {
        private static readonly string smtpServerAddress = ConfigurationManager.AppSettings["smtp_server_address"];
        private static readonly string email = ConfigurationManager.AppSettings["email"];
        private static readonly string emailPassword = ConfigurationManager.AppSettings["email_password"];
        private static readonly string emailName = ConfigurationManager.AppSettings["email_name"];

        public async Task NeedApproval()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(emailName, email));
            message.To.AddRange(GetHrs().Select(GetMailboxAddress));
            message.Cc.Add(GetMailboxAddress(Account.CurrentUser.Email));
            message.Cc.AddRange(GetAdmins().Select(GetMailboxAddress));
            message.Subject = string.Format("-	EmailTitle: [PCI Union Intranet]User: {0}'s membership need your approval", Account.CurrentUser.GetName());
            message.Body = new TextPart("plain")
                           {
                               Text = message.Subject
                           };
            await Send(message);
        }

        private static async Task Send(MimeMessage message)
        {
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(smtpServerAddress);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                if (!string.IsNullOrEmpty(emailPassword))
                {
                    await client.AuthenticateAsync(GetName(email), emailPassword);
                }
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }

        private static MailboxAddress GetMailboxAddress(string e)
        {
            return new MailboxAddress(GetName(e), e);
        }

        private static string GetName(string e)
        {
            return new string(e.TakeWhile(m => m != '@').ToArray());
        }

        private IEnumerable<string> GetHrs()
        {
            var hrs = Account.GetHrs();
            return hrs.Select(h => h.Email).ToList();
        }

        private IEnumerable<string> GetAdmins()
        {
            var admins = Account.GetAdmins();
            return admins.Select(a => a.Email).ToList();
        }

        public async Task Approved(User user)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(emailName, email));
            message.Cc.AddRange(GetHrs().Select(GetMailboxAddress));
            message.Cc.AddRange(GetAdmins().Select(GetMailboxAddress));
            message.To.Add(GetMailboxAddress(user.Email));
            message.Subject = string.Format("[PCI Union Intranet] {0}: Your request of Union membership has been approved", user.GetName());
            message.Body = new TextPart("plain")
                           {
                               Text = message.Subject
                           };
            await Send(message);
        }

        public async Task Rejected(User user)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(emailName, email));
            message.Cc.AddRange(GetHrs().Select(GetMailboxAddress));
            message.Cc.AddRange(GetAdmins().Select(GetMailboxAddress));
            message.To.Add(GetMailboxAddress(user.Email));
            message.Subject = string.Format("[PCI Union Intranet] {0}: Your request of Union membership has been rejected", user.GetName());
            message.Body = new TextPart("plain")
                           {
                               Text = message.Subject
                           };
            await Send(message);
        }

        public async Task InformStatusChanged(User user)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(emailName, email));
            message.To.AddRange(GetHrs().Select(GetMailboxAddress));
            message.To.AddRange(GetAdmins().Select(GetMailboxAddress));
            message.To.Add(GetMailboxAddress(user.Email));
            message.Subject = string.Format("[PCI website]User: {0}’s membership status is changed to \"{1}\"", user.GetName(), user.Status);
            message.Body = new TextPart("plain")
                           {
                               Text = message.Subject
                           };
            await Send(message);
        }
    }
}