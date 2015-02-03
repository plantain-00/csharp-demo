using System.Web.Mvc;

namespace TokenBasedWebsiteDemo.Controllers
{
    public class HomeController : BaseController
    {
        [TokenAuthorizeAttribute]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Login(string userName, string password)
        {
            
        }
    }
}