using Ioc.Net.Model.Models;

namespace Ioc.Net.Model.Businesses
{
    public class UserBusiness : BaseBusiness, IUserBusiness
    {
        public User[] GetUsers()
        {
            return UserService.GetUsers();
        }

        public void AddUsers(params string[] names)
        {
            if (names == null
                || names.Length == 0)
            {
                return;
            }
            foreach (var name in names)
            {
                UserService.AddUser(new User
                                    {
                                        Name = name
                                    });
            }
            UserService.Entities.SaveChanges();
        }
    }
}