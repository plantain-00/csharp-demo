using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Bootstrap.Pagination;

using MailKit.Net.Smtp;

using MimeKit;

namespace MailKit.Template.Demo.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ViewData["name"] = "Monica";

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("mailkit_test", "mailkit_test@163.com"));
            message.To.Add(new MailboxAddress("yaoyao12306", "yaoyao12306@163.com"));
            message.Subject = this.PartialViewToString("NotifySubject");
            message.Body = new TextPart("plain")
            {
                Text = this.PartialViewToString("NotifyBody")
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.163.com");
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate("mailkit_test", "mailkit_password");

                client.Send(message);
                client.Disconnect(true);
            }
            return View();
        }

    }
}
