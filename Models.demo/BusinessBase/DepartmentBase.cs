using Models.demo.DbModels;

namespace Models.demo.BusinessBase
{
    public class DepartmentBase
    {
        public DepartmentBase()
        {
        }

        public DepartmentBase(Department department)
        {
            Id = department.Id;
            Name = department.Name;
            FatherId = department.FatherId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? FatherId { get; set; }
    }
}