namespace TokenBasedWebsiteDemo.DbModels
{
    public class User
    {
        public int ID { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Salt { get; set; }
    }
}