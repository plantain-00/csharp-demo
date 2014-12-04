using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using BCL.Extension;

using Bootstrap.Pagination;

using BPM.Data;
using BPM.Service.Models;

namespace BPM.Service
{
    public class AccountService : IAccountService
    {
        /// <summary>
        ///     RealName,UserId
        /// </summary>
        public event Action<string, string> OnLoginSuccessfully;

        public JsonResultModel Login(string name, string password)
        {
            JsonResultModel json;
            try
            {
                using (var entities = new Entities())
                {
                    if (entities.Users.Any(u => u.Name == name))
                    {
                        var salt = entities.Users.First(u => u.Name == name).Salt;
                        var encodedPassword = Encode(password, salt);
                        var existed = entities.Users.Any(u => u.Name == name && u.Password == encodedPassword);
                        if (existed)
                        {
                            var user = entities.Users.FirstOrDefault(u => u.Name == name);
                            if (user.Can(Permission.Login))
                            {
                                if (OnLoginSuccessfully != null)
                                {
                                    OnLoginSuccessfully(user.RealName, user.ID);
                                }
                                json = new JsonResultModel(true);
                            }
                            else
                            {
                                json = new JsonResultModel(false, "该用户没有登陆权限");
                            }
                        }
                        else
                        {
                            json = new JsonResultModel(false, "用户名或密码错误");
                        }
                    }
                    else
                    {
                        json = new JsonResultModel(false, "用户名或密码错误");
                    }
                }
            }
            catch (Exception exception)
            {
                json = new JsonResultModel(false, "未知错误", exception.FullMessage());
            }
            return json;
        }

        public JsTreeNode GetAllUserAndDepartments()
        {
            var root = new JsTreeNode(string.Empty, "组织")
                       {
                           State = new JsTreeNodeState(true)
                       };
            using (var entities = new Entities())
            {
                foreach (var department in entities.Departments.Where(d => d.ParentID == null).ToArray())
                {
                    root.Children.Add(new JsTreeNode(department));
                }
                foreach (var user in entities.Users.Where(u => u.DepartmentId == null).ToArray())
                {
                    var userNode = new JsTreeNode(user.ID, user.Name)
                                   {
                                       Icon = "Images/user.png"
                                   };
                    root.Children.Add(userNode);
                    foreach (var role in user.Roles)
                    {
                        var roleNode = new JsTreeNode(Guid.NewGuid().ToString(), role.Name)
                                       {
                                           Icon = "Images/role.png"
                                       };
                        userNode.Children.Add(roleNode);
                        foreach (var permissionClass in role.GetPermissionClasses())
                        {
                            var permissionClassNode = new JsTreeNode(Guid.NewGuid().ToString(), permissionClass);
                            roleNode.Children.Add(permissionClassNode);
                            var allPermission = Protocols.AllPermissions[permissionClass];
                            foreach (var permission in role.RolePermissions.Select(rp => rp.Permission).Where(allPermission.Contains))
                            {
                                permissionClassNode.Children.Add(new JsTreeNode(Guid.NewGuid().ToString(), Protocols.DisplayedPermissions[permission])
                                                                 {
                                                                     Icon = "Images/permission.png"
                                                                 });
                            }
                        }
                    }
                }
            }
            return root;
        }

        public JsTreeNode GetAllPermissions()
        {
            var root = new JsTreeNode(string.Empty, Protocols.ALL_PERMISSIONS)
                       {
                           State = new JsTreeNodeState(true)
                       };
            foreach (var pair in Protocols.AllPermissions)
            {
                var node = new JsTreeNode(Guid.NewGuid().ToString(), pair.Key);
                root.Children.Add(node);
                foreach (var permission in pair.Value)
                {
                    node.Children.Add(new JsTreeNode(Guid.NewGuid().ToString(), Protocols.DisplayedPermissions[permission])
                                      {
                                          Icon = "Images/permission.png"
                                      });
                }
            }
            return root;
        }

        public List<RoleDTO> GetAllRoles()
        {
            using (var entities = new Entities())
            {
                var roles = entities.Roles.MapToDTO().ToList();
                return roles;
            }
        }

        public List<DepartmentDTO> GetAllDepartments()
        {
            using (var entities = new Entities())
            {
                var departments = entities.Departments.MapToDTO();
                return departments;
            }
        }

        public Tuple<Pagination, List<UserDTO>> UserManagement(string name, string realName, int page, int group, string departmentId)
        {
            Func<IQueryable<User>, IQueryable<User>> nameCondition = set =>
                                                                     {
                                                                         if (string.IsNullOrEmpty(name))
                                                                         {
                                                                             return set;
                                                                         }
                                                                         return set.Where(s => s.Name.Contains(name));
                                                                     };
            Func<IQueryable<User>, IQueryable<User>> realNameCondition = set =>
                                                                         {
                                                                             if (string.IsNullOrEmpty(realName))
                                                                             {
                                                                                 return set;
                                                                             }
                                                                             return set.Where(s => s.RealName.Contains(realName));
                                                                         };
            using (var entities = new Entities())
            {
                List<string> userIds;
                if (string.IsNullOrEmpty(departmentId))
                {
                    userIds = entities.Users.Select(d => d.ID).ToList();
                }
                else
                {
                    var allDepartments = entities.Departments.MapToDTO();
                    userIds = allDepartments.FirstOrDefault(d => d.ID == departmentId).GetUsers();
                }
                var totalCount = entities.Users.Choose(nameCondition).Choose(realNameCondition).Count(u => userIds.Contains(u.ID));
                var pagination = new Pagination(totalCount, page, group, 5, 10);
                var users = entities.Users.Choose(nameCondition).Choose(realNameCondition).Where(u => userIds.Contains(u.ID)).OrderBy(p => p.Name).Skip(pagination.ItemIndex).Take(10).MapToDTO();
                return Tuple.Create(pagination, users);
            }
        }

        public JsonResultModel ModifyUser(string name, string id, string password, string realName, string departmentId, IList<string> roleIds)
        {
            JsonResultModel json;
            using (var entities = new Entities())
            {
                if (entities.Users.Any(c => c.Name == name && c.ID != id))
                {
                    json = new JsonResultModel(false, string.Format("该用户名称已存在：{0}", name));
                }
                else if (string.IsNullOrEmpty(id))
                {
                    try
                    {
                        var salt = Guid.NewGuid().ToString();
                        var encodedPassword = Encode(password, salt);
                        var user = new User
                                   {
                                       ID = Guid.NewGuid().ToString(),
                                       Name = name,
                                       Password = encodedPassword,
                                       RealName = realName,
                                       DepartmentId = departmentId,
                                       Salt = salt
                                   };
                        entities.Users.Add(user);
                        user.Roles.Clear();
                        foreach (var roleId in roleIds)
                        {
                            user.Roles.Add(entities.Roles.FirstOrDefault(p => p.ID == roleId));
                        }
                        entities.SaveChanges();
                        json = new JsonResultModel(true, string.Format("添加用户成功：{0}", name));
                    }
                    catch (Exception exception)
                    {
                        json = new JsonResultModel(false, string.Format("添加用户失败：{0}", name), exception.Message);
                    }
                }
                else
                {
                    try
                    {
                        var toBeModified = entities.Users.First(c => c.ID == id);
                        if (Protocols.IsBuildInUser(toBeModified.Name))
                        {
                            json = new JsonResultModel(false, string.Format("不能修改内置用户:{0}", toBeModified.Name));
                        }
                        else
                        {
                            var originalName = toBeModified.Name;
                            toBeModified.Name = name;
                            if (!string.IsNullOrEmpty(password))
                            {
                                var encodedPassword = Encode(password, toBeModified.Salt);
                                toBeModified.Password = encodedPassword;
                            }
                            toBeModified.RealName = realName;
                            toBeModified.DepartmentId = departmentId;
                            toBeModified.Roles.Clear();
                            foreach (var roleId in roleIds)
                            {
                                toBeModified.Roles.Add(entities.Roles.FirstOrDefault(p => p.ID == roleId));
                            }
                            entities.SaveChanges();
                            json = new JsonResultModel(true, string.Format("修改用户成功：{0}->{1}", originalName, name));
                        }
                    }
                    catch (Exception exception)
                    {
                        json = new JsonResultModel(false, string.Format("修改用户失败：{0}", name), exception.FullMessage());
                    }
                }
            }
            return json;
        }

        public UserDTO GetUser(string id)
        {
            using (var entities = new Entities())
            {
                var user = entities.Users.Include(u => u.Roles).Include(u => u.Roles.Select(r => r.RolePermissions)).FirstOrDefault(c => c.ID == id);
                return user.MapToDTO();
            }
        }

        public JsonResultModel DeleteUser(string id, string userName)
        {
            JsonResultModel json;
            try
            {
                using (var entities = new Entities())
                {
                    var toBeDeleted = entities.Users.FirstOrDefault(p => p.ID == id);
                    var count = toBeDeleted.WorkAssignmentsAsApplier.Count + toBeDeleted.WorkAssignmentsAsFinancialUser.Count + toBeDeleted.WorkAssignmentsAsTestUser.Count;
                    if (Protocols.IsBuildInUser(toBeDeleted.Name))
                    {
                        json = new JsonResultModel(false, "不能删除内置用户");
                    }
                    else if (count > 0)
                    {
                        json = new JsonResultModel(false, string.Format("请先修改或删除该用户参与的{0}个流程记录", count));
                    }
                    else
                    {
                        if (toBeDeleted.Name == userName)
                        {
                            json = new JsonResultModel(false, "不能删除当前用户");
                        }
                        else
                        {
                            toBeDeleted.Roles.Clear();
                            entities.Users.Remove(toBeDeleted);
                            entities.SaveChanges();
                            json = new JsonResultModel(true, "删除用户成功");
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                json = new JsonResultModel(false, "删除用户失败", exception.FullMessage());
            }
            return json;
        }

        public Tuple<Pagination, List<DepartmentDTO>> DepartmentManagement(string name, string departmentId, int page, int group)
        {
            Func<IQueryable<Department>, IQueryable<Department>> nameCondiftion = set =>
                                                                                  {
                                                                                      if (string.IsNullOrEmpty(name))
                                                                                      {
                                                                                          return set;
                                                                                      }
                                                                                      return set.Where(s => s.Name.Contains(name));
                                                                                  };
            using (var entities = new Entities())
            {
                var allDepartments = entities.Departments.MapToDTO();
                List<string> childDepartmentIds;
                if (string.IsNullOrEmpty(departmentId))
                {
                    childDepartmentIds = allDepartments.Select(d => d.ID).ToList();
                }
                else
                {
                    childDepartmentIds = allDepartments.FirstOrDefault(d => d.ID == departmentId).GetChildDepartments();
                }
                var totalCount = entities.Departments.Choose(nameCondiftion).Count(d => childDepartmentIds.Contains(d.ID));
                var pagination = new Pagination(totalCount, page, group, 5, 10);
                var departments = entities.Departments.Choose(nameCondiftion).Where(d => childDepartmentIds.Contains(d.ID)).OrderBy(p => p.Name).Skip(pagination.ItemIndex).Take(10).MapToDTO();
                return Tuple.Create(pagination, departments);
            }
        }

        public JsonResultModel ModifyDepartment(string id, string name, string parentDepartmentId, IList<string> roleIds)
        {
            JsonResultModel json;
            using (var entities = new Entities())
            {
                if (entities.Departments.Any(c => c.Name == name && c.ID != id))
                {
                    json = new JsonResultModel(false, string.Format("该部门已存在：{0}", name));
                }
                else if (string.IsNullOrEmpty(id))
                {
                    try
                    {
                        var department = new Department
                                         {
                                             ID = Guid.NewGuid().ToString(),
                                             Name = name,
                                             ParentID = parentDepartmentId
                                         };
                        entities.Departments.Add(department);
                        department.Roles.Clear();
                        foreach (var roleId in roleIds)
                        {
                            department.Roles.Add(entities.Roles.FirstOrDefault(p => p.ID == roleId));
                        }
                        entities.SaveChanges();
                        json = new JsonResultModel(true, string.Format("添加部门成功：{0}", name));
                    }
                    catch (Exception exception)
                    {
                        json = new JsonResultModel(false, string.Format("添加部门失败：{0}", name), exception.FullMessage());
                    }
                }
                else
                {
                    try
                    {
                        var toBeModified = entities.Departments.First(c => c.ID == id);
                        if (Protocols.IsBuildInDepartment(toBeModified.Name))
                        {
                            json = new JsonResultModel(false, string.Format("不能修改内置部门:{0}", toBeModified.Name));
                        }
                        else
                        {
                            var originalName = toBeModified.Name;
                            toBeModified.Name = name;
                            toBeModified.ParentID = parentDepartmentId;
                            toBeModified.Roles.Clear();
                            foreach (var roleId in roleIds)
                            {
                                toBeModified.Roles.Add(entities.Roles.FirstOrDefault(p => p.ID == roleId));
                            }
                            entities.SaveChanges();
                            json = new JsonResultModel(true, string.Format("修改部门成功：{0}->{1}", originalName, name));
                        }
                    }
                    catch (Exception exception)
                    {
                        json = new JsonResultModel(false, string.Format("修改部门失败：{0}", name), exception.FullMessage());
                    }
                }
            }
            return json;
        }

        public DepartmentDTO GetDepartment(string id)
        {
            using (var entities = new Entities())
            {
                var departments = entities.Departments.MapToDTO();
                return departments.FirstOrDefault(c => c.ID == id);
            }
        }

        public JsonResultModel DeleteDepartment(string id)
        {
            JsonResultModel json;
            try
            {
                using (var entities = new Entities())
                {
                    var toBeDeleted = entities.Departments.FirstOrDefault(p => p.ID == id);
                    var count = toBeDeleted.Departments.Count;
                    if (Protocols.IsBuildInDepartment(toBeDeleted.Name))
                    {
                        json = new JsonResultModel(false, "不能删除内置部门");
                    }
                    else if (count > 0)
                    {
                        json = new JsonResultModel(false, string.Format("请先修改或删除该类别下的{0}个部门", count));
                    }
                    else
                    {
                        toBeDeleted.Roles.Clear();
                        entities.Departments.Remove(toBeDeleted);
                        entities.SaveChanges();
                        json = new JsonResultModel(true, "删除部门成功");
                    }
                }
            }
            catch (Exception exception)
            {
                json = new JsonResultModel(false, "删除部门失败", exception.FullMessage());
            }
            return json;
        }

        public Tuple<Pagination, List<RoleDTO>> RoleManagement(string name, int page, int group)
        {
            Func<IQueryable<Role>, IQueryable<Role>> condition = set =>
                                                                 {
                                                                     if (string.IsNullOrEmpty(name))
                                                                     {
                                                                         return set;
                                                                     }
                                                                     return set.Where(s => s.Name.Contains(name));
                                                                 };
            using (var entities = new Entities())
            {
                var totalCount = entities.Roles.Choose(condition).Count();
                var pagination = new Pagination(totalCount, page, group, 5, 10);
                var roles = entities.Roles.Choose(condition).OrderBy(p => p.Name).Skip(pagination.ItemIndex).Take(10).MapToDTO();
                return Tuple.Create(pagination, roles);
            }
        }

        public JsonResultModel ModifyRole(string id, string name, IList<string> permissionIds)
        {
            JsonResultModel json;
            using (var entities = new Entities())
            {
                if (entities.Roles.Any(c => c.Name == name && c.ID != id))
                {
                    json = new JsonResultModel(false, string.Format("该角色名称已存在：{0}", name));
                }
                else if (string.IsNullOrEmpty(id))
                {
                    try
                    {
                        var role = new Role
                                   {
                                       ID = Guid.NewGuid().ToString(),
                                       Name = name
                                   };
                        entities.Roles.Add(role);
                        role.RolePermissions.Clear();
                        foreach (var permissionId in permissionIds)
                        {
                            role.RolePermissions.Add(new RolePermission(role.ID, (Permission) Convert.ToInt32(permissionId)));
                        }
                        entities.SaveChanges();
                        json = new JsonResultModel(true, string.Format("添加角色成功：{0}", name));
                    }
                    catch (Exception exception)
                    {
                        json = new JsonResultModel(false, string.Format("添加角色失败：{0}", name), exception.FullMessage());
                    }
                }
                else
                {
                    try
                    {
                        var toBeModified = entities.Roles.First(c => c.ID == id);
                        if (Protocols.IsBuildInRole(toBeModified.Name))
                        {
                            json = new JsonResultModel(false, string.Format("不能修改内置角色:{0}", toBeModified.Name));
                        }
                        else
                        {
                            var originalName = toBeModified.Name;
                            toBeModified.RolePermissions.Clear();
                            foreach (var permissionId in permissionIds)
                            {
                                toBeModified.RolePermissions.Add(new RolePermission(toBeModified.ID, (Permission) Convert.ToInt32(permissionId)));
                            }
                            toBeModified.Name = name;
                            entities.SaveChanges();
                            json = new JsonResultModel(true, string.Format("修改角色成功：{0}->{1}", originalName, name));
                        }
                    }
                    catch (Exception exception)
                    {
                        json = new JsonResultModel(false, string.Format("修改角色失败：{0}", name), exception.FullMessage());
                    }
                }
            }
            return json;
        }

        public RoleDTO GetRole(string id)
        {
            using (var entities = new Entities())
            {
                var role = entities.Roles.FirstOrDefault(c => c.ID == id);
                return role.MapToDTO();
            }
        }

        public JsonResultModel DeleteRole(string id)
        {
            JsonResultModel json;
            try
            {
                using (var entities = new Entities())
                {
                    var toBeDeleted = entities.Roles.FirstOrDefault(p => p.ID == id);
                    if (Protocols.IsBuildInRole(toBeDeleted.Name))
                    {
                        json = new JsonResultModel(false, "不能删除内置角色");
                    }
                    else
                    {
                        toBeDeleted.RolePermissions.Clear();
                        toBeDeleted.Departments.Clear();
                        toBeDeleted.Users.Clear();
                        entities.Roles.Remove(toBeDeleted);
                        entities.SaveChanges();
                        json = new JsonResultModel(true, "删除角色成功");
                    }
                }
            }
            catch (Exception exception)
            {
                json = new JsonResultModel(false, "删除角色失败", exception.FullMessage());
            }
            return json;
        }

        public JsonResultModel ModifyPassword(string oldPassword, string newPassword, string userId)
        {
            JsonResultModel json;
            if (string.IsNullOrEmpty(oldPassword))
            {
                json = new JsonResultModel(false, "旧密码不能为空");
            }
            else if (string.IsNullOrEmpty(newPassword))
            {
                json = new JsonResultModel(false, "新密码不能为空");
            }
            else
            {
                try
                {
                    using (var entities = new Entities())
                    {
                        var user = entities.Users.FirstOrDefault(u => u.ID == userId);
                        if (user.Password != Encode(oldPassword))
                        {
                            json = new JsonResultModel(false, "旧密码错误");
                        }
                        else
                        {
                            user.Password = Encode(newPassword);
                            entities.SaveChanges();
                            json = new JsonResultModel(true, "修改密码成功");
                        }
                    }
                }
                catch (Exception exception)
                {
                    json = new JsonResultModel(false, "修改密码失败", exception.FullMessage());
                }
            }
            return json;
        }

        private static string Encode(string password, string salt = null)
        {
            var passwordWithSalt = salt == null ? password : password + salt;
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(passwordWithSalt))).Replace("-", "");
        }
    }
}