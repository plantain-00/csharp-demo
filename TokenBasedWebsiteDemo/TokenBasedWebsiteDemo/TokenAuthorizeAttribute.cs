using System;
using System.Web.Mvc;

using RetrievePassword;

using TokenBasedWebsiteDemo.Services;

namespace TokenBasedWebsiteDemo
{
    public class TokenAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            var token = filterContext.HttpContext.Request["token"];
            if (string.IsNullOrEmpty(token))
            {
                filterContext.Result = RedirectToLoginPage(filterContext);
                return;
            }

            var tmp = token.Split('|');
            var result = tmp[0];
            var seconds = tmp[1];
            var userId = tmp[2];

            if (string.IsNullOrEmpty(userId))
            {
                filterContext.Result = RedirectToLoginPage(filterContext);
                return;
            }
            var uid = Convert.ToInt32(userId, 16);

            string password;

            using (var accountService = new AccountService())
            {
                var user = accountService.GetUserById(uid);
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
            filterContext.Result = null;
        }

        private static RedirectResult RedirectToLoginPage(ControllerContext filterContext)
        {
            return new RedirectResult(new UrlHelper(filterContext.RequestContext).Action("Login", "Home"));
        }
    }
}