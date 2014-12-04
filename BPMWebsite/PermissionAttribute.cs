using System.Net;
using System.Web.Mvc;

using BPM.Data;
using BPM.Service;

namespace BPMWebsite
{
    public class PermissionAttribute : AuthorizeAttribute
    {
        public Permission[] Permission;

        public PermissionAttribute(params Permission[] permission)
        {
            Permission = permission;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            var name = filterContext.RequestContext.HttpContext.Request.Cookies[Protocols.Cookie.NAME].Value;
            if (name.HasAny(Permission))
            {
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
        }
    }
}