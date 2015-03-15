using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

using Bootstrap.Pagination;

using ExcelFile.net;

using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

using Vex.DbModels;

namespace Vex.Controllers
{
    public class AccountController : BaseController
    {
        [Authentication(Constants.REGISTER)]
        public ActionResult Register()
        {
            var currentUser = Base.Value.Account.Value.GetCurrentUser();
            if (currentUser == null)
            {
                ViewData["WorkingLocations"] = Account.Value.WorkingLocations;
                ViewData["Sectors"] = Account.Value.Sectors;
                ViewData["BusinessFunlocs"] = Account.Value.BusinessFunlocs;
                ViewData["CostCenters"] = Account.Value.CostCenters;
                ViewData["email"] = Account.Value.GetEmailByAccountName(HttpContext.User.Identity.Name);
                return View();
            }

            var validator = new UserValidator();
            var result = validator.Validate(currentUser);
            if (!result.IsValid)
            {
                return RedirectToAction("ModifyMyProfile", "Account");
            }

            return RedirectToAction("Index", "Home");
        }

        [Authentication(Constants.REGISTER)]
        [HttpPost]
        public async Task<ActionResult> Register(User user)
        {
            var validator = new UserValidator();
            var result = validator.Validate(user);
            if (!result.IsValid)
            {
                var message = "Error:";
                foreach (var error in result.Errors)
                {
                    message += error;
                }
                return Content(string.Format("<script>alert(\"{0}\"); window.history.back();</script>", message));
            }

            await Account.Value.Register(user);

            await Mail.Value.NeedApproval();

            return RedirectToAction("Index", "Home");
        }


        [Authentication(Constants.MODIFY_PROFILE)]
        public ActionResult ModifyProfile(int id)
        {
            ViewData["WorkingLocations"] = Account.Value.WorkingLocations;
            ViewData["Sectors"] = Account.Value.Sectors;
            ViewData["BusinessFunlocs"] = Account.Value.BusinessFunlocs;
            ViewData["CostCenters"] = Account.Value.CostCenters;

            var user = Account.Value.GetUser(id);
            ViewData["user"] = user;
            ViewData["currentUser"] = Account.Value.CurrentUser;

            return View();
        }

        public ActionResult ModifyMyProfile()
        {
            ViewData["WorkingLocations"] = Account.Value.WorkingLocations;
            ViewData["Sectors"] = Account.Value.Sectors;
            ViewData["BusinessFunlocs"] = Account.Value.BusinessFunlocs;
            ViewData["CostCenters"] = Account.Value.CostCenters;

            ViewData["user"] = Account.Value.CurrentUser;
            ViewData["currentUser"] = Account.Value.CurrentUser;

            return View("ModifyProfile");
        }

        [Authentication(Constants.MODIFY_PROFILE)]
        [HttpPost]
        public async Task<ActionResult> ModifyProfile(User user)
        {
            var validator = new UserValidator();
            var result = validator.Validate(user);
            if (!result.IsValid)
            {
                var message = "Error:";
                foreach (var error in result.Errors)
                {
                    message += error;
                }
                return Content(string.Format("<script>alert(\"{0}\"); window.history.back();</script>", message));
            }

            await Account.Value.ModifyProfile(user);

            if (Account.Value.CurrentUser.Id != user.Id)
            {
                return Content(string.Format("<script>alert(\"success.\"); location.href='{0}';</script>",
                                             Url.Action("ModifyProfile",
                                                        new
                                                        {
                                                            ID = user.Id
                                                        })));
            }
            return Content(string.Format("<script>alert(\"success.\"); location.href='{0}';</script>", Url.Action("ModifyMyProfile")));
        }


        [Authentication(Constants.MODIFY_STATUS)]
        [HttpPost]
        public async Task<ActionResult> ModifyStatus(int id, UserStatus status)
        {
            try
            {
                var result = await Account.Value.ModifyStatus(id, status);

                if (result.Item1 == UserStatus.Applied)
                {
                    switch (result.Item2.Status)
                    {
                        case UserStatus.Active:
                            await Mail.Value.Approved(result.Item2);
                            break;
                        case UserStatus.Inactive:
                            await Mail.Value.Rejected(result.Item2);
                            break;
                        default:
                            await Mail.Value.InformStatusChanged(result.Item2);
                            break;
                    }
                }
                else
                {
                    await Mail.Value.InformStatusChanged(result.Item2);
                }

                return Json(new
                            {
                                isSuccess = true
                            });
            }
            catch (Exception exception)
            {
                return Json(new
                            {
                                isSuccess = false,
                                message = exception.Message
                            });
            }
        }


        [Authentication(Constants.MODIFY_STATUS)]
        public ActionResult BatchUnregistration()
        {
            return View();
        }

        [Authentication(Constants.MODIFY_STATUS)]
        [HttpPost]
        public async Task<ActionResult> GetImportedEmails()
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (!string.IsNullOrEmpty(file.FileName))
                {
                    var extension = Path.GetExtension(file.FileName);
                    if (!extension.Equals(".xlsx", StringComparison.CurrentCultureIgnoreCase))
                    {
                        ViewData["message"] = "not a xlsx file.";
                        ViewData["isSuccess"] = false as bool?;
                        return View("BatchUnregistration");
                    }

                    IWorkbook excel = new XSSFWorkbook(file.InputStream);
                    if (excel.Count == 0)
                    {
                        ViewData["message"] = "no sheet in the excel.";
                        ViewData["isSuccess"] = false as bool?;
                        return View("BatchUnregistration");
                    }

                    var sheet = excel[0];

                    var emails = new List<string>();

                    try
                    {
                        for (var i = 0; i <= sheet.LastRowNum; i++)
                        {
                            var row = sheet.GetRow(i);
                            if (row == null)
                            {
                                continue;
                            }

                            var cell = row.GetCell(0);
                            if (cell.CellType == CellType.String)
                            {
                                var email = cell.GetString();
                                if (!string.IsNullOrEmpty(email))
                                {
                                    emails.Add(email);
                                }
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        if (exception is ExcelDataException)
                        {
                            var e = exception as ExcelDataException;
                            ViewData["message"] = string.Format("error happens at row : {0} and column : {1} .", e.RowIndex + 1, e.ColumnIndex + 1);
                            ViewData["isSuccess"] = false as bool?;
                            return View("BatchUnregistration");
                        }
                    }

                    try
                    {
                        var users = await Account.Value.Unregister(emails);

                        foreach (var user in users)
                        {
                            await Mail.Value.InformStatusChanged(user);
                        }

                        ViewData["users"] = users;

                        ViewData["message"] = "success.";
                        ViewData["isSuccess"] = true as bool?;
                        return View("BatchUnregistration");
                    }
                    catch (Exception exception)
                    {
                        ViewData["message"] = exception.Message;
                        ViewData["isSuccess"] = false as bool?;
                        return View("BatchUnregistration");
                    }
                }
                ViewData["message"] = "no file name.";
                ViewData["isSuccess"] = false as bool?;
                return View("BatchUnregistration");
            }
            ViewData["message"] = "no file.";
            ViewData["isSuccess"] = false as bool?;
            return View("BatchUnregistration");
        }


        [Authentication(Constants.MEMBER_QUERY_AND_EXPORT)]
        public ActionResult MemberList()
        {
            int count;
            var skipped = Pagination.GetSkipped(1);
            var users = Account.Value.GetEmployees(skipped, 10, out count);
            ViewData["pagination"] = new Pagination(count, 1);
            ViewData["users"] = users;

            ViewData["WorkingLocations"] = Account.Value.WorkingLocations;
            ViewData["Sectors"] = Account.Value.Sectors;
            ViewData["BusinessFunlocs"] = Account.Value.BusinessFunlocs;
            ViewData["CostCenters"] = Account.Value.CostCenters;

            return View();
        }

        [Authentication(Constants.MEMBER_QUERY_AND_EXPORT)]
        public ActionResult ExportMembers(int page, string name, string gender, string year, string month, string day, string employeeId, string email, string mobilePhone, string location, string sector, string funloc, string costCenter, string status, bool isDesc, string column)
        {
            int count;
            var theGender = string.IsNullOrEmpty(gender) ? null as bool? : Convert.ToBoolean(gender);
            var theYear = string.IsNullOrEmpty(year) ? null as int? : Convert.ToInt32(year);
            var theMonth = string.IsNullOrEmpty(month) ? null as int? : Convert.ToInt32(month);
            var theDay = string.IsNullOrEmpty(day) ? null as int? : Convert.ToInt32(day);
            var theLocation = string.IsNullOrEmpty(location) ? null as int? : Convert.ToInt32(location);
            var theSector = string.IsNullOrEmpty(sector) ? null as int? : Convert.ToInt32(sector);
            var theFunloc = string.IsNullOrEmpty(funloc) ? null as int? : Convert.ToInt32(funloc);
            var theCostCenter = string.IsNullOrEmpty(costCenter) ? null as int? : Convert.ToInt32(costCenter);
            var theStatus = string.IsNullOrEmpty(status) ? null as UserStatus? : (UserStatus) Convert.ToInt32(status);
            var members = Account.Value.GetEmployees(0, int.MaxValue, out count, name, theGender, theYear, theMonth, theDay, employeeId, email, mobilePhone, theLocation, theSector, theFunloc, theCostCenter, theStatus, isDesc, column);

            var excel = new ExcelEditor(Base.Value.GetPath("/Templates/Export Members.xlsx"));
            excel.Set("m",
                      members.Select(m => new
                                          {
                                              m.ChineseName,
                                              EnglishName = m.GetEnglishName(),
                                              m.NTAccount,
                                              Gender = m.Gender ? "男 / Male" : "女 / Female",
                                              m.Birthday,
                                              m.Birthday_Year,
                                              m.Birthday_Month,
                                              m.Birthday_Day,
                                              m.EmployeeId,
                                              m.Email,
                                              m.PersonalEmail,
                                              m.MobilePhone,
                                              m.FixedPhone,
                                              m.OfficePhone,
                                              m.EmergencyContact,
                                              m.EmergencyPhone,
                                              m.OfficeAddress,
                                              m.DeliveryAddress,
                                              WorkingLocation = m.WorkingLocation.Name,
                                              Sector = m.Sector.Name,
                                              BusinessFunloc = m.BusinessFunloc.Name,
                                              CostCenter = m.CostCenter.Name,
                                              m.PersonalInterest,
                                              m.RegisterTime
                                          }).ToArray());
            excel.Save(Response, string.Format("Union Members exported at {0}.xlsx", DateTime.Now.ToString("yyy-MM-dd HHmmss")));
            return new ContentResult();
        }

        [Authentication(Constants.MEMBER_QUERY_AND_EXPORT)]
        public ActionResult GetMemberList(int page, string name, string gender, string year, string month, string day, string employeeId, string email, string mobilePhone, string location, string sector, string funloc, string costCenter, string status, bool isDesc, string column)
        {
            int count;
            var skipped = Pagination.GetSkipped(page);

            var theGender = string.IsNullOrEmpty(gender) ? null as bool? : Convert.ToBoolean(gender);
            var theYear = string.IsNullOrEmpty(year) ? null as int? : Convert.ToInt32(year);
            var theMonth = string.IsNullOrEmpty(month) ? null as int? : Convert.ToInt32(month);
            var theDay = string.IsNullOrEmpty(day) ? null as int? : Convert.ToInt32(day);
            var theLocation = string.IsNullOrEmpty(location) ? null as int? : Convert.ToInt32(location);
            var theSector = string.IsNullOrEmpty(sector) ? null as int? : Convert.ToInt32(sector);
            var theFunloc = string.IsNullOrEmpty(funloc) ? null as int? : Convert.ToInt32(funloc);
            var theCostCenter = string.IsNullOrEmpty(costCenter) ? null as int? : Convert.ToInt32(costCenter);
            var theStatus = string.IsNullOrEmpty(status) ? null as UserStatus? : (UserStatus) Convert.ToInt32(status);
            var users = Account.Value.GetEmployees(skipped, 10, out count, name, theGender, theYear, theMonth, theDay, employeeId, email, mobilePhone, theLocation, theSector, theFunloc, theCostCenter, theStatus, isDesc, column);

            ViewData["pagination"] = new Pagination(count, page);
            ViewData["users"] = users;
            return View();
        }
    }
}