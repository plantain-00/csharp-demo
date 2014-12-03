using Models.demo.DbModels;

namespace Models.demo.BusinessBase
{
    public class UserBase
    {
        public UserBase()
        {
        }

        public UserBase(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Age = user.Age;
            DepartmentId = user.DepartmentId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int? DepartmentId { get; set; }
    }
}