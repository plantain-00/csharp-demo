using System.Web.Mvc;

using RetrievePassword;

using TokenBasedWebsiteDemo.Services;

namespace TokenBasedWebsiteDemo.Controllers
{
    public class HomeController : BaseController
    {
        [TokenAuthorize]
        public ActionResult Index()
        {
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
                var retriever = new Retriever();
                string seconds;
                var result = retriever.Generate(user.Password, out seconds);
                return RedirectToAction("Index",
                                        new
                                        {
                                            token = result + 'G' + seconds + 'G' + user.ID.ToString("x")
                                        });
            }
            return RedirectToAction("Login");
        }
    }
}