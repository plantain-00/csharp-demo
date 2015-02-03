using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

using RetrievePassword;

using TokenBasedWebsiteDemo.DbModels;

namespace TokenBasedWebsiteDemo
{
    public class TokenAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var result = filterContext.HttpContext.Request["r"];
            var seconds = filterContext.HttpContext.Request["s"];
            var userId = filterContext.HttpContext.Request["u"];

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
                    throw new Exception();
                }
            }

            var retriever = new Retriever();
            var isValid = retriever.IsValid(password, seconds, result, new TimeSpan(0, 20, 0));
            if (isValid)
            {
                
            }

            base.OnAuthorization(filterContext);
        }
    }
}