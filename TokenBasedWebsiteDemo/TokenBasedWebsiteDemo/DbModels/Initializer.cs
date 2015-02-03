using System.Data.Entity;

using Despise;

using TokenBasedWebsiteDemo.Services;

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
                var salt = generator.Get<PasswordGenerator>().Generate();
                var md5Service = new Md5Service();
                context.Users.Add(new User
                                  {
                                      Name = generator.Get<EnglishWordGenerator>().Generate(),
                                      Salt = salt,
                                      Password = md5Service.Md5(1 + salt)
                                  });
            }

            base.Seed(context);
        }
    }
}