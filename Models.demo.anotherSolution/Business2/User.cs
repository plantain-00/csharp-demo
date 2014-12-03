using System.Collections.Generic;

using Models.demo.anotherSolution.DbModels;

namespace Models.demo.anotherSolution.Business2
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public IEnumerable<Role> Roles { get; set; }
    }
}