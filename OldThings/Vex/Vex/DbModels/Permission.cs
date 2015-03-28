using System.Collections.Generic;

namespace Vex.DbModels
{
    public class Permission
    {
        public Permission()
        {
            Roles = new HashSet<Role>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}