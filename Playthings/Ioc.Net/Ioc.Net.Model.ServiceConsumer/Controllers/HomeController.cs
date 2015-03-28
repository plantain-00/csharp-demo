using System.Web.Mvc;

using Ioc.Net.Model.ServiceConsumer.ServiceReference;

namespace Ioc.Net.Model.ServiceConsumer.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            var userServiceClient = new UserServiceClient();
            var users = userServiceClient.GetUsers();
            ViewData["users"] = users;
            return View();
        }
    }
}