using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using TokenBasedWebsiteDemo.DbModels;

namespace TokenBasedWebsiteDemo.Services
{
    public class AccountService : DisposableService
    {
        public async Task<User> GetUserByNameAsync(string name)
        {
            return await Entities.Users.FirstOrDefaultAsync(u => u.Name == name);
        }

        public User GetUserById(int id)
        {
            return Entities.Users.FirstOrDefault(u => u.ID == id);
        }
    }
}