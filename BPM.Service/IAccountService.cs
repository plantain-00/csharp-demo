using System;
using System.Collections.Generic;

using Bootstrap.Pagination;

using BPM.Service.Models;

namespace BPM.Service
{
    public interface IAccountService
    {
        /// <summary>
        ///     RealName,UserId
        /// </summary>
        event Action<string, string> OnLoginSuccessfully;

        JsonResultModel Login(string name, string password);
        JsTreeNode GetAllUserAndDepartments();
        JsTreeNode GetAllPermissions();
        List<RoleDTO> GetAllRoles();
        List<DepartmentDTO> GetAllDepartments();
        Tuple<Pagination, List<UserDTO>> UserManagement(string name, string realName, int page, int group, string departmentId);
        JsonResultModel ModifyUser(string name, string id, string password, string realName, string departmentId, IList<string> roleIds);
        UserDTO GetUser(string id);
        JsonResultModel DeleteUser(string id, string userName);
        Tuple<Pagination, List<DepartmentDTO>> DepartmentManagement(string name, string departmentId, int page, int group);
        JsonResultModel ModifyDepartment(string id, string name, string parentDepartmentId, IList<string> roleIds);
        DepartmentDTO GetDepartment(string id);
        JsonResultModel DeleteDepartment(string id);
        Tuple<Pagination, List<RoleDTO>> RoleManagement(string name, int page, int group);
        JsonResultModel ModifyRole(string id, string name, IList<string> permissionIds);
        RoleDTO GetRole(string id);
        JsonResultModel DeleteRole(string id);
        JsonResultModel ModifyPassword(string oldPassword, string newPassword, string userId);
    }
}