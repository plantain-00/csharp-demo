using System.Web.Mvc;

namespace jsonp.provider.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return new JsonpResult
                   {
                       Data = new
                              {
                                  Name = "Irving",
                                  Age = 23
                              },
                       JsonRequestBehavior = JsonRequestBehavior.AllowGet
                   };
        }
    }
}