using System;
using System.Web.Mvc;

namespace Ioc.Net.Model.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            UserBusiness.AddUsers("test");
            var users = UserBusiness.GetUsers();
            //var currentUser = UserBusiness.UserService.CurrentUser;
            //if (currentUser == null)
            //{
            //    throw new Exception();
            //}
            ViewData["users"] = users;
            //var name = UserBusiness.UserService.GetSession<string>("name");
            //if (name == null)
            //{
            //    throw new Exception();
            //}
            return View();
        }

        public ActionResult AddUsers(params string[] names)
        {
            UserBusiness.AddUsers(names);
            return new ContentResult();
        }
    }
}