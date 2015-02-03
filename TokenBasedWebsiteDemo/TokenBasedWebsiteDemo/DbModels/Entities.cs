using System.Data.Entity;

namespace TokenBasedWebsiteDemo.DbModels
{
    public class Entities : DbContext
    {
        public Entities() : base("name=Entities")
        {
            Database.SetInitializer(new Initializer());
        }

        public virtual DbSet<User> Users { get; set; }
    }
}