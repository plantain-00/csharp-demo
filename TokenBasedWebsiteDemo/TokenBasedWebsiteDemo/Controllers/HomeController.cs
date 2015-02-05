using System.Web.Mvc;

using TokenBasedWebsiteDemo.Services;

namespace TokenBasedWebsiteDemo.Controllers
{
    public class HomeController : BaseController
    {
        [TokenAuthorize]
        public ActionResult Index(string token)
        {
            var userId = Get<TokenService>().GetUserId(token);
            var currentUser = Get<AccountService>().GetCurrentUser(userId);
            ViewData["newToken"] = Get<TokenService>().Generate(currentUser.Password, userId);
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DoLogin(string userName, string password)
        {
            var user = Get<AccountService>().GetUserByName(userName);
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            if (user.Password == Get<Md5Service>().Md5(password + user.Salt))
            {
                return RedirectToAction("Index",
                                        new
                                        {
                                            token = Get<TokenService>().Generate(user.Password, user.ID)
                                        });
            }
            return RedirectToAction("Login");
        }
    }
}