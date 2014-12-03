using System;
using System.Activities;
using System.Linq;
using System.Web.Mvc;

using WFWithCodeActivity.Models;

namespace WFWithCodeActivity.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            using (var entities = new Entities())
            {
                ViewData["demos"] = entities.WorkflowDemos.ToArray();
            }
            return View();
        }

        public ActionResult A()
        {
            using (var entities = new Entities())
            {
                ViewData["demos"] = entities.WorkflowDemos.ToArray();
            }
            return View();
        }

        public ActionResult ASubmit()
        {
            var money = Convert.ToInt32(Request.Form["money"]);
            var reason = Request.Form["reason"];
            var workflowApplication = new WorkflowApplication(new TestWorkflow());

            using (var entities = new Entities())
            {
                entities.WorkflowDemos.Add(new WorkflowDemo
                                           {
                                               Money = money,
                                               Reason = reason,
                                               Submiter = "A",
                                               SubmitDate = DateTime.Now,
                                               ID = workflowApplication.Id,
                                               Status = "待B审核"
                                           });
                entities.SaveChanges();
            }

            workflowApplication.Run();
            MvcApplication.WorkflowApplications.Add(workflowApplication);
            workflowApplication.Completed += delegate
                                             {
                                                 MvcApplication.WorkflowApplications.Remove(workflowApplication);
                                             };
            return RedirectToAction("A");
        }

        public ActionResult B()
        {
            using (var entities = new Entities())
            {
                ViewData["demos"] = entities.WorkflowDemos.ToArray();
            }
            return View();
        }

        public ActionResult C()
        {
            using (var entities = new Entities())
            {
                ViewData["demos"] = entities.WorkflowDemos.ToArray();
            }
            return View();
        }
    }
}