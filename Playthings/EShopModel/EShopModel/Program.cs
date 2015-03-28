namespace EShopModel
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var entities = new Entities())
            {
                entities.Customers.Add(new Customer
                                       {
                                           Name = "customer 1"
                                       });
                entities.SaveChanges();
            }
        }
    }
}