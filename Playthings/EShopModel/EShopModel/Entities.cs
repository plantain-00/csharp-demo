using System.Collections.Generic;
using System.Data.Entity;

namespace EShopModel
{
    public class Entities : DbContext
    {
        public Entities() : base("name=Entities")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<Entities>());
        }

        public virtual DbSet<Vendor> Vendors { get; set; }
        public virtual DbSet<Commodity> Commodities { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
    }

    public class Vendor
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Commodity> Commodities { get; set; }
    }

    public class Commodity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Vendor> Vendors { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int CustomerId { get; set; }

        public virtual ICollection<Commodity> Commodities { get; set; }
        public virtual Customer Customer { get; set; }
    }

    public class Cart
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }

        public virtual ICollection<Commodity> Commodities { get; set; }
        public virtual Customer Customer { get; set; }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
    }
}