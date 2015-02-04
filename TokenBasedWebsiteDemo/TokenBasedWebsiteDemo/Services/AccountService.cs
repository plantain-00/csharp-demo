using System;
using System.Collections.Generic;
using System.Linq;

using TokenBasedWebsiteDemo.DbModels;

namespace TokenBasedWebsiteDemo.Services
{
    public class AccountService : DisposableService
    {
        public User GetUserByName(string name, DataSourceType dataSourceType = DataSourceType.AspNetCache)
        {
            if (dataSourceType == DataSourceType.AspNetCache)
            {
                var users = GetCacheSafely<List<User>>("Users", new TimeSpan(0, 20, 0));
                var user = users.FirstOrDefault(u => u.Name == name);
                if (user == null)
                {
                    user = Entities.Users.FirstOrDefault(u => u.Name == name);
                    if (user != null)
                    {
                        users.Add(user);
                    }
                }
                return user;
            }
            return Entities.Users.FirstOrDefault(u => u.Name == name);
        }

        public User GetUserById(int id, DataSourceType dataSourceType = DataSourceType.AspNetCache)
        {
            if (dataSourceType == DataSourceType.AspNetCache)
            {
                var users = GetCacheSafely<List<User>>("Users", new TimeSpan(0, 20, 0));
                var user = users.FirstOrDefault(u => u.ID == id);
                if (user == null)
                {
                    user = Entities.Users.FirstOrDefault(u => u.ID == id);
                    if (user != null)
                    {
                        users.Add(user);
                    }
                }
                return user;
            }
            return Entities.Users.FirstOrDefault(u => u.ID == id);
        }

        public User GetCurrentUser(int id)
        {
            var cacheName = "CurrentUser_" + id.ToString("X");
            if (HasCache<User>(cacheName))
            {
                return GetCache<User>(cacheName);
            }
            var user = Entities.Users.FirstOrDefault(u => u.ID == id);
            SetCache(cacheName, user, new TimeSpan(0, 20, 0));
            return user;
        }
    }
}