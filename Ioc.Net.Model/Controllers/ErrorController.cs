using System.Web.Mvc;

namespace Ioc.Net.Model.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ActionResult Index()
        {
            return new ContentResult();
        }
    }
}