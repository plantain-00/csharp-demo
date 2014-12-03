using Models.demo.BusinessBase;

namespace Models.demo.Business2
{
    public class Department : DepartmentBase
    {
        public Department()
        {
        }

        public Department(DbModels.Department department)
        {
            if (department.Father != null)
            {
                Father = new Department(department.Father);
            }
        }

        public Department Father { get; set; }
    }
}