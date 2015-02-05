using System;
using System.Linq;

using TokenBasedWebsiteDemo.DbModels;

namespace TokenBasedWebsiteDemo.Services
{
    public class AccountService : DisposableService
    {
        public User GetUserByName(string name)
        {
            return Entities.Users.FirstOrDefault(u => u.Name == name);
        }

        public User GetUserById(int id, DataSourceType dataSourceType = DataSourceType.AspNetCache)
        {
            Func<User> query = () => Entities.Users.FirstOrDefault(u => u.ID == id);
            return Query("User_" + id.ToString("X"), dataSourceType, new TimeSpan(0, 20, 0), query);
        }
    }
}