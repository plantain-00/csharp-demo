using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

using ExcelFile.net;

using Ioc.Net.Model.Models;

namespace Ioc.Net.Model.Services
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“UserService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 UserService.svc 或 UserService.svc.cs，然后开始调试。
    public class UserService : BaseService, IUserService
    {
        public User[] GetUsers()
        {
            if (!HasCache<User[]>("users"))
            {
                SetCache("users", Entities.Users.ToArray(), new TimeSpan(0, 20, 0));
            }
            return GetCache<User[]>("users");
        }

        public IQueryable<User> GetUsers<T>(out int count, int skipped = 0, bool isAsc = true, Expression<Func<User, T>> order = null, params Expression<Func<User, bool>>[] conditions)
        {
            IQueryable<User> query = Entities.Users;
            foreach (var condition in conditions)
            {
                query = query.Where(condition);
            }
            count = query.Count();
            if (order == null)
            {
                query = query.OrderBy(u => u.ID);
            }
            else if (isAsc)
            {
                query = query.OrderBy(order);
            }
            else
            {
                query = query.OrderByDescending(order);
            }
            return query.Skip(skipped).Take(10);
        }

        public void Export(User[] users)
        {
            var excel = new ExcelEditor("/Templates/Users.xlsx");
            excel.Set("users", users);
            ExportExcel(excel, string.Format("Users {0}.xlsx", DateTime.Now.ToString("yyyyMMddHHmmss")));
        }

        public List<User> Import(string fileName)
        {
            var result = new List<User>();
            var path = GetPath("~/UploadedFiles/" + fileName);
            foreach (var sheet in ExcelUtils.New(path, FileMode.Open, FileAccess.Read).AsEnumerable())
            {
                for (var i = 1; i < sheet.LastRowNum; i++)
                {
                    var row = sheet.GetRow(i);
                    if (row == null)
                    {
                        continue;
                    }
                    result.Add(new User
                               {
                                   ID = (int) row.GetCell(0).GetNumber(),
                                   Name = row.GetCell(1).GetString()
                               });
                }
            }
            return result;
        }

        public void AddUser(User user)
        {
            Entities.Users.Add(user);
        }
    }
}