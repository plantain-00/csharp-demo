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
            var md5Service = new Md5Service();
            for (var i = 0; i < 20; i++)
            {
                var salt = generator.Get<PasswordGenerator>().Generate();
                if (i == 0)
                {
                    context.Users.Add(new User
                                      {
                                          Name = "test",
                                          Salt = salt,
                                          Password = md5Service.Md5(1 + salt)
                                      });
                }
                else
                {
                    context.Users.Add(new User
                                      {
                                          Name = generator.Get<EnglishWordGenerator>().Generate(),
                                          Salt = salt,
                                          Password = md5Service.Md5(generator.Get<PasswordGenerator>().Generate() + salt)
                                      });
                }
            }

            base.Seed(context);
        }
    }
}