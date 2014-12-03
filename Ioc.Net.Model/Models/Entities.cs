using System.Data.Entity;

namespace Ioc.Net.Model.Models
{
    public class Entities : DbContext
    {
        public Entities() : base("name=Entities")
        {
        }

        public DbSet<User> Users { get; set; }
    }
}