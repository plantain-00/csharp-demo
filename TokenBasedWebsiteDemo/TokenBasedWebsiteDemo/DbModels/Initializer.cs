using System.Data.Entity;

using Despise;

namespace TokenBasedWebsiteDemo.DbModels
{
    /// <summary>
    ///     DropCreateDatabaseIfModelChanges
    ///     CreateDatabaseIfNotExists
    ///     DropCreateDatabaseAlways
    /// </summary>
    public class Initializer : DropCreateDatabaseIfModelChanges<Entities>
    {
        protected override void Seed(Entities context)
        {
            var generator = new Generator();
            for (var i = 0; i < 10; i++)
            {
                context.Users.Add(new User
                                  {
                                      Name = generator.Get<EnglishWordGenerator>().Generate(),
                                      Password = generator.Get<PasswordGenerator>().Generate()
                                  });
            }
            
            base.Seed(context);
        }
    }
}