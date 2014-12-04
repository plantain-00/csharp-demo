using System;
using System.Web.Mvc;

using Bootstrap.Pagination;

using BPM.Data;
using BPM.Service;

namespace BPMWebsite.Controllers
{
    public class ProcessController : Controller
    {
        private readonly IProcessService _processService;

        public ProcessController(IProcessService processService)
        {
            _processService = processService;
        }

        [HttpGet]
        [Permission(Permission.WorkDistribution)]
        public ActionResult WorkAssignmentStart()
        {
            ViewData["Title"] = "工作安排/派发工作";
            ViewData["assigner"] = Request.Cookies[Protocols.Cookie.NAME].Value;
            ViewData["departments"] = _processService.WorkAssignmentStart();
            return View();
        }

        [HttpPost]
        [Permission(Permission.WorkDistribution)]
        public ActionResult WorkAssignmentStartSubmit()
        {
            var financialUserID = Request.Form["financial"];
            var testUserID = Request.Form["test"];
            var applierID = Request.Cookies[Protocols.Cookie.USER_ID].Value;
            var projectID = Request.Form["projectID"];
            var projectName = Request.Form["projectName"];
            var workContent = Request.Form["workContent"];
            var reportDate = Convert.ToDateTime(Request.Form["reportDate"]);
            var remarks = Request.Form["remarks"];
            var json = _processService.WorkAssignmentStartSubmit(financialUserID, testUserID, applierID, projectID, projectName, reportDate, workContent, remarks);
            return this.NewtonJson(json);
        }

        [HttpGet]
        [Authorize]
        public ActionResult WorkAssignmentHandle()
        {
            var id = Request.QueryString["id"];
            var userId = Request.Cookies[Protocols.Cookie.USER_ID].Value;
            var workAssignmentDTO = _processService.WorkAssignmentHandle(id);
            TempData["workAssignmentDTO"] = workAssignmentDTO;
            switch (workAssignmentDTO.GetStatus(userId))
            {
                case "财务部":
                    return RedirectToAction("WorkAssignmentFinancial");
                case "化验室":
                    return RedirectToAction("WorkAssignmentTest");
                case "查看完成情况":
                    return RedirectToAction("WorkAssignmentCheck");
                default:
                    throw new Exception();
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult WorkAssignmentFinancial()
        {
            ViewData["workAssignmentDTO"] = TempData["workAssignmentDTO"];
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult WorkAssignmentFinancialSubmit()
        {
            var id = Request.Form["id"];
            var financialSituation = Request.Form["financialSituation"];
            var financialDelayReason = Request.Form["financialDelayReason"];
            var financialFinishDate = Convert.ToDateTime(Request.Form["financialFinishDate"]);
            var json = _processService.WorkAssignmentFinancialSubmit(id, financialSituation, financialDelayReason, financialFinishDate);
            return this.NewtonJson(json);
        }

        [HttpGet]
        [Authorize]
        public ActionResult WorkAssignmentTest()
        {
            ViewData["workAssignmentDTO"] = TempData["workAssignmentDTO"];
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult WorkAssignmentTestSubmit()
        {
            var id = Request.Form["id"];
            var testSituation = Request.Form["testSituation"];
            var testDelayReason = Request.Form["testDelayReason"];
            var testFinishDate = Convert.ToDateTime(Request.Form["testFinishDate"]);
            var json = _processService.WorkAssignmentTestSubmit(id, testSituation, testDelayReason, testFinishDate);
            return this.NewtonJson(json);
        }

        [HttpGet]
        [Authorize]
        public ActionResult WorkAssignmentCheck()
        {
            ViewData["workAssignmentDTO"] = TempData["workAssignmentDTO"];
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult WorkAssignmentCheckSubmit()
        {
            var id = Request.Form["id"];
            var json = _processService.WorkAssignmentCheckSubmit(id);
            return this.NewtonJson(json);
        }

        [HttpGet]
        [Authorize]
        public ActionResult WorkAssignmentDetail()
        {
            var id = Request.QueryString["id"];
            ViewData["workAssignmentDTO"] = _processService.WorkAssignmentDetail(id);
            return View();
        }
    }
}