using System.Data.Entity;

namespace Models.demo.anotherSolution.DbModels
{
    public class Entities : DbContext
    {
        public Entities() : base("name=Entities")
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<Entities>());
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
    }
}