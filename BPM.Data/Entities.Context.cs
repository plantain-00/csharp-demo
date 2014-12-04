using System.Data.Entity;

namespace BPM.Data
{
    public class Entities : DbContext
    {
        public Entities() : base("name=Entities")
        {
            Database.SetInitializer(new Initializer());
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WorkAssignment> WorkAssignments { get; set; }
        public DbSet<Read> Reads { get; set; }
        public DbSet<Draft> Drafts { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}