using System.Collections.Generic;
using System.Linq;

using Models.demo.BusinessBase;

namespace Models.demo.Business1
{
    public class Department : DepartmentBase
    {
        public Department()
        {
        }

        public Department(DbModels.Department department) : base(department)
        {
            Users = department.Users.Select(u => new UserBase(u)).ToArray();
            Departments = department.Departments.Select(d => new Department(d)).ToArray();
        }

        public IList<UserBase> Users { get; set; }
        public IList<Department> Departments { get; set; }
    }
}