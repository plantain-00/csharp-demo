using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using Bootstrap.Pagination;

using BPM.Data;
using BPM.Service;

using Newtonsoft.Json;

namespace BPMWebsite.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public ActionResult Login()
        {
            var returnUrl = Request.QueryString["ReturnUrl"];
            ViewData["returnUrl"] = string.IsNullOrEmpty(returnUrl) ? Url.Action("Index", "Home") : returnUrl;
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult JsonLogin()
        {
            var name = Request.Form["name"];
            var password = Request.Form["password"];
            _accountService.OnLoginSuccessfully += (realName, userId) =>
                                                   {
                                                       Response.Cookies.Add(new HttpCookie(Protocols.Cookie.NAME, name));
                                                       Response.Cookies.Add(new HttpCookie(Protocols.Cookie.REALNAME, HttpUtility.UrlEncode(realName)));
                                                       Response.Cookies.Add(new HttpCookie(Protocols.Cookie.USER_ID, userId));
                                                       FormsAuthentication.SetAuthCookie(name, false);
                                                   };
            var json = _accountService.Login(name, password);
            return this.NewtonJson(json);
        }

        [HttpGet]
        public ActionResult Exit()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [HttpGet]
        [Permission(Permission.AllUsers)]
        public ActionResult AllUserAndDepartments()
        {
            return View();
        }

        [HttpGet]
        [Permission(Permission.AllUsers)]
        public ActionResult GetAllUserAndDepartments()
        {
            var root = _accountService.GetAllUserAndDepartments();
            return this.NewtonJson(root, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Permission(Permission.AllPermissions)]
        public ActionResult AllPermissions()
        {
            return View();
        }

        [HttpGet]
        [Permission(Permission.AllPermissions)]
        public ActionResult GetAllPermissions()
        {
            var root = _accountService.GetAllPermissions();
            return this.NewtonJson(root, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Permission(Permission.UserManagement)]
        public ActionResult UserManagement()
        {
            var page = Request.QueryInt32("page");
            var group = Request.QueryInt32("group");
            var name = Request.QueryString["name"];
            ViewData["name"] = name;
            var realName = Request.QueryString["realName"];
            ViewData["realName"] = realName;
            var departmentId = Request.QueryString["departmentId"];
            ViewData["departmentId"] = departmentId;
            ViewData["allDepartments"] = _accountService.GetAllDepartments();
            var result = _accountService.UserManagement(name, realName, page, group, departmentId);
            ViewData["pagination"] = result.Item1;
            ViewData["users"] = result.Item2;
            ViewData["allRoles"] = _accountService.GetAllRoles();
            return View();
        }

        [HttpPost]
        [Permission(Permission.UserManagement)]
        public ActionResult JsonModifyUser()
        {
            var name = Request.Form["name"];
            var id = Request.Form["id"];
            var password = Request.Form["password"];
            var realName = Request.Form["realName"];
            var department = Request.Form["department"] == string.Empty ? null : Request.Form["department"];
            var roleIds = JsonConvert.DeserializeObject<List<string>>(Request.Form["roles"]);
            var json = _accountService.ModifyUser(name, id, password, realName, department, roleIds);
            return this.NewtonJson(json);
        }

        [HttpGet]
        [Permission(Permission.UserManagement)]
        public ActionResult JsonGetUser()
        {
            var id = Request.QueryString["id"];
            var userDTO = _accountService.GetUser(id);
            return this.NewtonJson(userDTO, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Permission(Permission.UserManagement)]
        public ActionResult JsonDeleteUser()
        {
            var id = Request.Form["id"];
            var json = _accountService.DeleteUser(id, Request.Cookies[Protocols.Cookie.NAME].Value);
            return this.NewtonJson(json);
        }

        [HttpGet]
        [Permission(Permission.DepartmentManagement)]
        public ActionResult DepartmentManagement()
        {
            var page = Request.QueryInt32("page");
            var group = Request.QueryInt32("group");
            var name = Request.QueryString["name"];
            ViewData["name"] = name;
            var departmentId = Request.QueryString["departmentId"];
            ViewData["departmentId"] = departmentId;
            var result = _accountService.DepartmentManagement(name, departmentId, page, group);
            ViewData["allDepartments"] = _accountService.GetAllDepartments();
            ViewData["pagination"] = result.Item1;
            ViewData["departments"] = result.Item2;
            ViewData["allRoles"] = _accountService.GetAllRoles();
            return View();
        }

        [HttpPost]
        [Permission(Permission.DepartmentManagement)]
        public ActionResult JsonModifyDepartment()
        {
            var name = Request.Form["name"];
            var id = Request.Form["id"];
            var parentDepartment = Request.Form["parentDepartment"] == string.Empty ? null : Request.Form["parentDepartment"];
            var roleIds = JsonConvert.DeserializeObject<List<string>>(Request.Form["roles"]);
            var json = _accountService.ModifyDepartment(id, name, parentDepartment, roleIds);
            return this.NewtonJson(json);
        }

        [HttpGet]
        [Permission(Permission.DepartmentManagement)]
        public ActionResult JsonGetDepartment()
        {
            var id = Request.QueryString["id"];
            var departmentDTO = _accountService.GetDepartment(id);
            return this.NewtonJson(departmentDTO, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Permission(Permission.DepartmentManagement)]
        public ActionResult JsonDeleteDepartment()
        {
            var id = Request.Form["id"];
            var json = _accountService.DeleteDepartment(id);
            return this.NewtonJson(json);
        }

        [HttpGet]
        [Permission(Permission.RoleManagement)]
        public ActionResult RoleManagement()
        {
            var page = Request.QueryInt32("page");
            var group = Request.QueryInt32("group");
            var name = Request.QueryString["name"];
            ViewData["name"] = name;
            var result = _accountService.RoleManagement(name, page, group);
            ViewData["pagination"] = result.Item1;
            ViewData["roles"] = result.Item2;
            return View();
        }

        [HttpPost]
        [Permission(Permission.RoleManagement)]
        public ActionResult JsonModifyRole()
        {
            var name = Request.Form["name"];
            var id = Request.Form["id"];
            var permissionIds = JsonConvert.DeserializeObject<List<string>>(Request.Form["permissions"]);
            var json = _accountService.ModifyRole(id, name, permissionIds);
            return this.NewtonJson(json);
        }

        [HttpGet]
        [Permission(Permission.RoleManagement)]
        public ActionResult JsonGetRole()
        {
            var id = Request.QueryString["id"];
            var roleDTO = _accountService.GetRole(id);
            return this.NewtonJson(roleDTO, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Permission(Permission.RoleManagement)]
        public ActionResult JsonDeleteRole()
        {
            var id = Request.Form["id"];
            var json = _accountService.DeleteRole(id);
            return this.NewtonJson(json);
        }

        [HttpGet]
        [Permission(Permission.ModifyPassword)]
        public ActionResult ModifyPassword()
        {
            return View();
        }

        [HttpPost]
        [Permission(Permission.ModifyPassword)]
        public ActionResult JsonModifyPassword()
        {
            var oldPassword = Request.Form["oldPassword"];
            var newPassword = Request.Form["newPassword"];
            var json = _accountService.ModifyPassword(oldPassword, newPassword, Request.Cookies[Protocols.Cookie.USER_ID].Value);
            return this.NewtonJson(json);
        }
    }
}