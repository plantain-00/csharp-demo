using System.Collections.Generic;

namespace Models.demo.DbModels
{
    public class Department
    {
        public Department()
        {
            Users = new HashSet<User>();
            Departments = new HashSet<Department>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? FatherId { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual Department Father { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
    }
}