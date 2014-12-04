using System.Collections.Generic;

namespace BPM.Data
{
    public class Role
    {
        public Role()
        {
            Departments = new HashSet<Department>();
            Users = new HashSet<User>();
            RolePermissions = new HashSet<RolePermission>();
        }

        public string ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}