using System.Collections.Generic;
using System.Linq;

using BPM.Data;
using BPM.Service.Models;

namespace BPM.Service
{
    public static class DTOMapper
    {
        public static List<DepartmentDTO> MapToDTO(this IQueryable<Department> departments)
        {
            var result = departments.Select(d => new DepartmentDTO
                                                 {
                                                     ID = d.ID,
                                                     Name = d.Name,
                                                     ParentID = d.ParentID,
                                                     Users = d.Users.Select(u => new UserDTO
                                                                                 {
                                                                                     ID = u.ID,
                                                                                     Name = u.Name,
                                                                                     Password = u.Password,
                                                                                     RealName = u.RealName,
                                                                                     DepartmentName = u.Department.Name
                                                                                 }),
                                                     Roles = d.Roles.Select(r => new RoleDTO
                                                                                 {
                                                                                     ID = r.ID,
                                                                                     Name = r.Name,
                                                                                     Permissions = r.RolePermissions.Select(rp => rp.Permission)
                                                                                 })
                                                 }).ToList();
            Refactor(ref result);
            return result;
        }

        public static List<RoleDTO> MapToDTO(this IQueryable<Role> roles)
        {
            return roles.Select(r => new RoleDTO
                                     {
                                         ID = r.ID,
                                         Name = r.Name,
                                         Permissions = r.RolePermissions.Select(rp => rp.Permission)
                                     }).ToList();
        }

        public static RoleDTO MapToDTO(this Role role)
        {
            return new RoleDTO
                   {
                       ID = role.ID,
                       Name = role.Name,
                       Permissions = role.RolePermissions.Select(rp => rp.Permission)
                   };
        }

        private static void Refactor(ref List<DepartmentDTO> departments)
        {
            foreach (var department in departments)
            {
                if (!string.IsNullOrEmpty(department.ParentID))
                {
                    var parent = departments.First(d => d.ID == department.ParentID);
                    if (parent.Departments == null)
                    {
                        parent.Departments = new List<DepartmentDTO>();
                    }
                    parent.Departments.Add(department);
                    department.ParentDepartmentName = parent.Name;
                }
            }
        }

        public static List<UserDTO> MapToDTO(this IQueryable<User> users)
        {
            return users.Select(u => new UserDTO
                                     {
                                         ID = u.ID,
                                         Name = u.Name,
                                         DepartmentName = u.Department == null ? null : u.Department.Name,
                                         Password = u.Password,
                                         RealName = u.RealName,
                                         Roles = u.Roles.Select(r => new RoleDTO
                                                                     {
                                                                         ID = r.ID,
                                                                         Name = r.Name,
                                                                         Permissions = r.RolePermissions.Select(rp => rp.Permission)
                                                                     })
                                     }).ToList();
        }

        public static UserDTO MapToDTO(this User user)
        {
            return new UserDTO
                   {
                       ID = user.ID,
                       DepartmentName = user.Department == null ? null : user.Department.Name,
                       Name = user.Name,
                       Password = user.Password,
                       RealName = user.RealName,
                       Roles = user.Roles.Select(r => new RoleDTO
                                                      {
                                                          ID = r.ID,
                                                          Name = r.Name,
                                                          Permissions = r.RolePermissions.Select(rp => rp.Permission)
                                                      })
                   };
        }

        public static WorkAssignmentDTO MapToDTO(this WorkAssignment workAssignment)
        {
            return new WorkAssignmentDTO
                   {
                       ID = workAssignment.ID,
                       ApplierID = workAssignment.ApplierID,
                       ApplierName = workAssignment.Applier.Name,
                       ApplyDate = workAssignment.ApplyDate,
                       FinancialDelayReason = workAssignment.FinancialDelayReason,
                       FinancialFinishDate = workAssignment.FinancialFinishDate,
                       FinancialSituation = workAssignment.FinancialSituation,
                       FinancialUserID = workAssignment.FinancialUserID,
                       FinancialUserName = workAssignment.FinancialUser.Name,
                       ProjectID = workAssignment.ProjectID,
                       ProjectName = workAssignment.ProjectName,
                       SheetID = workAssignment.SheetID,
                       WorkContent = workAssignment.WorkContent,
                       ReportDate = workAssignment.ReportDate,
                       Remarks = workAssignment.Remarks,
                       Status = workAssignment.Status,
                       TestDelayReason = workAssignment.TestDelayReason,
                       TestFinishDate = workAssignment.TestFinishDate,
                       TestSituation = workAssignment.TestSituation,
                       TestUserID = workAssignment.TestUserID,
                       TestUserName = workAssignment.TestUser.Name
                   };
        }

        public static List<WorkAssignmentDTO> MapToDTO(this IQueryable<WorkAssignment> workAssignments)
        {
            return workAssignments.Select(w => new WorkAssignmentDTO
                                               {
                                                   ID = w.ID,
                                                   ApplierID = w.ApplierID,
                                                   ApplierName = w.Applier.Name,
                                                   ApplyDate = w.ApplyDate,
                                                   FinancialDelayReason = w.FinancialDelayReason,
                                                   FinancialFinishDate = w.FinancialFinishDate,
                                                   FinancialSituation = w.FinancialSituation,
                                                   FinancialUserID = w.FinancialUserID,
                                                   FinancialUserName = w.FinancialUser.Name,
                                                   ProjectID = w.ProjectID,
                                                   ProjectName = w.ProjectName,
                                                   SheetID = w.SheetID,
                                                   WorkContent = w.WorkContent,
                                                   ReportDate = w.ReportDate,
                                                   Remarks = w.Remarks,
                                                   Status = w.Status,
                                                   TestDelayReason = w.TestDelayReason,
                                                   TestFinishDate = w.TestFinishDate,
                                                   TestSituation = w.TestSituation,
                                                   TestUserID = w.TestUserID,
                                                   TestUserName = w.TestUser.Name
                                               }).ToList();
        }

        public static List<ReadDTO> MapToDTO(this IQueryable<Read> reads)
        {
            return reads.Select(r => new ReadDTO
                                     {
                                         ID = r.ID,
                                         ProcessID = r.ProcessID,
                                         SendDate = r.SendDate,
                                         SenderName = r.Sender.Name
                                     }).ToList();
        }
    }
}