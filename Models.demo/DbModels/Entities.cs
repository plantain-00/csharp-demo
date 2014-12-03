using System.Data.Entity;

namespace Models.demo.DbModels
{
    public class Entities : DbContext
    {
        public Entities() : base("name=Entities")
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<Entities>());
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
    }
}