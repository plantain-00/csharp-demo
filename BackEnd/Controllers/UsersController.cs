using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BackEnd.Controllers
{
    public class UsersController : ApiController
    {
        public IEnumerable<User> Get()
        {
            using (var entities = new MyEntitiesContainer())
            {
                return entities.UserSet.AsEnumerable().ToArray();
            }
        }
        public User Get(string id)
        {
            using (var entities = new MyEntitiesContainer())
            {
                return entities.UserSet.First(u => u.Id == id);
            }
        }
        public void Post([FromBody] User user)
        {
            using (var entities = new MyEntitiesContainer())
            {
                entities.UserSet.Add(user);
                entities.SaveChanges();
            }
        }
        public void Put(string id, [FromBody] User user)
        {
            using (var entities = new MyEntitiesContainer())
            {
                if (entities.UserSet.Any(u => u.Id == id))
                {
                    var oldUser= entities.UserSet.First(u => u.Id == id);
                    oldUser.Name = user.Name;
                    oldUser.Birthday = user.Birthday;
                }
                else
                {
                    entities.UserSet.Add(user);
                }
                entities.SaveChanges();
            }
        }
        public void Delete(string id)
        {
            using (var entities = new MyEntitiesContainer())
            {
                if (entities.UserSet.Any(u => u.Id == id))
                {
                    var user = entities.UserSet.First(u => u.Id == id);
                    entities.UserSet.Remove(user);
                    entities.SaveChanges();
                }
            }
        }
    }
}