using System.Web;
using System.Web.Mvc;

using Vex.Services;

namespace Vex
{
    public class AuthenticationAttribute : AuthorizeAttribute
    {
        public string[] Permissions;


        public AuthenticationAttribute(params string[] permissions)
        {
            Permissions = permissions;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            var accountService = new AccountService();

            if (accountService.CurrentUser == null
                || accountService.CurrentUser.Can(Permissions))
            {
                filterContext.Result = null;
            }
            else
            {
                throw new HttpException(403, "current user does not have the permission.");
            }
        }
    }
}