using Models.demo.BusinessBase;

namespace Models.demo.Business2
{
    public class User : UserBase
    {
        public User()
        {
        }

        public User(DbModels.User user) : base(user)
        {
            Department = new Department(user.Department);
        }

        public virtual Department Department { get; set; }
    }
}