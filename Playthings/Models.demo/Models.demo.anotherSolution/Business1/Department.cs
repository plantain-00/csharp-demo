using System.Collections.Generic;

namespace Models.demo.anotherSolution.Business1
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? FatherId { get; set; }
        public IEnumerable<DbModels.User> Users { get; set; }
    }
}