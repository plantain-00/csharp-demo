using System;
using System.Linq;
using System.Web.Mvc;

using RetrievePassword;

using TokenBasedWebsiteDemo.DbModels;

namespace TokenBasedWebsiteDemo
{
    public class TokenAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            var result = filterContext.HttpContext.Request["r"];
            var seconds = filterContext.HttpContext.Request["s"];
            var userId = filterContext.HttpContext.Request["u"];

            if (string.IsNullOrEmpty(userId))
            {
                filterContext.Result = RedirectToLoginPage(filterContext);
                return;
            }
            var uid = Convert.ToInt32(userId);

            string password;

            using (var entities = new Entities())
            {
                var user = entities.Users.FirstOrDefault(u => u.ID == uid);
                if (user != null)
                {
                    password = user.Password;
                }
                else
                {
                    filterContext.Result = RedirectToLoginPage(filterContext);
                    return;
                }
            }

            var retriever = new Retriever();
            var isValid = retriever.IsValid(password, seconds, result, new TimeSpan(0, 20, 0));
            if (!isValid)
            {
                filterContext.Result = RedirectToLoginPage(filterContext);
                return;
            }
        }

        private static RedirectResult RedirectToLoginPage(ControllerContext filterContext)
        {
            return new RedirectResult(new UrlHelper(filterContext.RequestContext).Action("Login", "Home"));
        }
    }
}