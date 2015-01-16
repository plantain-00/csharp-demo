using System.Collections.Generic;

namespace Vex.DbModels
{
    public class BusinessFunloc
    {
        public BusinessFunloc()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}