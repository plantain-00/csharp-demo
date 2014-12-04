using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

using Bootstrap.Pagination;

using BPM.Data;
using BPM.Service;

using Newtonsoft.Json;

namespace BPMWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            var name = Request.Cookies[Protocols.Cookie.NAME].Value;
            var result = _homeService.Index(name);
            ViewData["permissions"] = result.Item1;
            ViewData["permissionClasses"] = result.Item2;
            return View();
        }

        [HttpGet]
        [Permission(Permission.NewApplication)]
        public ActionResult NewApplication()
        {
            var name = Request.Cookies[Protocols.Cookie.NAME].Value;
            ViewData["permissions"] = _homeService.NewApplication(name);
            return View();
        }

        [HttpGet]
        [Permission(Permission.ToDoList)]
        public ActionResult ToDoList()
        {
            var page = Request.QueryInt32("page");
            var group = Request.QueryInt32("group");
            var userId = Request.Cookies[Protocols.Cookie.USER_ID].Value;
            var result = _homeService.ToDoList(userId, page, group);
            ViewData["pagination"] = result.Item1;
            ViewData["workAssignments"] = result.Item2;
            return View();
        }

        [HttpGet]
        [Permission(Permission.History)]
        public ActionResult History()
        {
            var page = Request.QueryInt32("page");
            var group = Request.QueryInt32("group");
            var userId = Request.Cookies[Protocols.Cookie.USER_ID].Value;
            var result = _homeService.History(userId, page, group);
            ViewData["pagination"] = result.Item1;
            ViewData["workAssignments"] = result.Item2;
            return View();
        }

        [HttpPost]
        [Permission(Permission.NewApplication)]
        public ActionResult Save()
        {
            var userId = Request.Cookies[Protocols.Cookie.USER_ID].Value;
            var json = _homeService.Save(userId, Request.Form);
            return this.NewtonJson(json);
        }

        [HttpPost]
        [Permission(Permission.NewApplication)]
        public ActionResult Recover()
        {
            var userId = Request.Cookies[Protocols.Cookie.USER_ID].Value;
            var names = JsonConvert.DeserializeObject<List<string>>(Request.Form["names"]);
            var result = _homeService.Recover(userId, names);
            return this.NewtonJson(result);
        }

        [HttpGet]
        [Permission(Permission.ToReadList)]
        public ActionResult UnreadList()
        {
            var page = Request.QueryInt32("page");
            var group = Request.QueryInt32("group");
            var userId = Request.Cookies[Protocols.Cookie.USER_ID].Value;
            var result = _homeService.UnreadList(userId, page, group);
            ViewData["pagination"] = result.Item1;
            ViewData["unreads"] = result.Item2;
            return View();
        }

        [HttpPost]
        [Permission(Permission.ToReadList)]
        public ActionResult Read()
        {
            var id = Request.Form["id"];
            var json = _homeService.Read(id);
            return this.NewtonJson(json);
        }

        [HttpGet]
        [Permission(Permission.NewApplication)]
        public ActionResult ProcessImage()
        {
            var process = MvcApplication.Process[0];
            var directory = Server.MapPath("/Images/Processes/" + process.Name + "/");
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            const string FILE_NAME = "开始.bmp";
            var file = directory + FILE_NAME;
            if (!System.IO.File.Exists(file))
            {
                MvcApplication.Set(process, "开始");
                MvcApplication.Set(process, "开始", "分配任务");
                MvcApplication.Set(process, "分配任务");
                process.ToBitmap().Save(file);
            }
            ViewData["src"] = string.Format(@"Images\Processes\{0}\{1}", process.Name, FILE_NAME);
            return View();
        }
    }
}