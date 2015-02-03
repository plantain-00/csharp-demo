using System.Web.Mvc;

namespace TokenBasedWebsiteDemo.Controllers
{
    [TokenAuthorizeAttribute]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}