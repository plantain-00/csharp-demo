using System.Collections.Generic;

namespace BPM.Data
{
    public class Department
    {
        public Department()
        {
            Departments = new HashSet<Department>();
            Roles = new HashSet<Role>();
            Users = new HashSet<User>();
        }

        public string ID { get; set; }
        public string Name { get; set; }
        public string ParentID { get; set; }
        
        public virtual ICollection<Department> Departments { get; set; }
        public virtual Department ParentDepartment { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}