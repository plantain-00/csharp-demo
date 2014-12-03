namespace Models.demo.anotherSolution.Business1
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int? DepartmentId { get; set; }
        public DbModels.Department Department { get; set; }
    }
}