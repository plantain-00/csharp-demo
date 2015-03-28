using System.Web.Mvc;

namespace Vex.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var currentUser = Account.Value.GetCurrentUser();
            if (currentUser == null)
            {
                return RedirectToAction("Register", "Account");
            }

            var validator = new UserValidator();
            var result = validator.Validate(currentUser);
            if (!result.IsValid)
            {
                return RedirectToAction("ModifyMyProfile", "Account");
            }

            ViewData["currentUser"] = currentUser;

            return View();
        }

        public ActionResult ClearSession()
        {
            Account.Value.ClearAllSession();
            return new ContentResult();
        }
    }
}