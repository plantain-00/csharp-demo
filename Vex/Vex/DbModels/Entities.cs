using System.Data.Entity;

namespace Vex.DbModels
{
    public class Entities : DbContext
    {
        public Entities() : base("name=Entities")
        {
            Database.SetInitializer(new Initializer());
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }

        public virtual DbSet<WorkingLocation> WorkingLocations { get; set; }
        public virtual DbSet<Sector> Sectors { get; set; }
        public virtual DbSet<BusinessFunloc> BusinessFunlocs { get; set; }
        public virtual DbSet<CostCenter> CostCenters { get; set; }
    }
}