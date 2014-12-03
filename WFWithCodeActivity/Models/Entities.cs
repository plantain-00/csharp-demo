using System.Data.Entity;

namespace WFWithCodeActivity.Models
{
    public class Entities : DbContext
    {
        public Entities() : base("name=Entities")
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<Entities>());
        }

        public virtual DbSet<WorkflowDemo> WorkflowDemos { get; set; }
    }
}