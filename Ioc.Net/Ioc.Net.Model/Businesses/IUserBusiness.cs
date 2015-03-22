using Ioc.Net.Model.Models;

namespace Ioc.Net.Model.Businesses
{
    public interface IUserBusiness : IBaseBusiness
    {
        User[] GetUsers();
        void AddUsers(params string[] names);
    }
}