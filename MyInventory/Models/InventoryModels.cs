using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MyInventory.Models
{
    public class InventoryContext : DbContext
    {
        public InventoryContext() : base("name=MyInventoryDBConnection")
        {
            Database.SetInitializer(new InventoryDBInitializer());
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CustomerOrder> CustomerOrders { get; set; }
        public DbSet<InventoryOrder> InventoryOrders { get; set; }
    }

    public class InventoryDBInitializer : DropCreateDatabaseIfModelChanges<InventoryContext>
    {
        protected override void Seed(InventoryContext context)
        {

            List<Customer> customers = new List<Customer>();
            customers.Add(new Customer { Id = Guid.NewGuid(), AddedDate = DateTime.Now, Name = "Super Depot", Address = "500 Depot Dr", City = "New Town", State = "NY", Phonenumber = "2123331515", Status=true,Zipcode= "23456" });
            customers.Add(new Customer { Id = Guid.NewGuid(), AddedDate = DateTime.Now, Name = "Best Prices", Address = "51 Price Ave", City = "Best Town", State = "MD", Phonenumber = "3013331515", Status = true, Zipcode = "22087" });
            context.Customers.AddRange(customers);

            List<Product> products = new List<Product>();
            products.Add(new Product { Id = Guid.NewGuid(), AddedDate = DateTime.Now, Description = "Big Hammer", LastUpdated = DateTime.Now, OnHand = 100, OrderPoint = 10, SalePrice = 4, Status = true, UPC = "11111"});
            context.Products.AddRange(products);

            Random rnd = new Random();

            List<InventoryOrder> io = new List<InventoryOrder>();
            io.Add(new InventoryOrder { Id = Guid.NewGuid(), Date = DateTime.Now, ProductId = products[0].Id, Quantity = 125, OrderNumber = rnd.Next(1000, 9999) });
            context.InventoryOrders.AddRange(io);
                        
            List<CustomerOrder> co = new List<CustomerOrder>();
            co.Add(new CustomerOrder {Id = Guid.NewGuid(), CustomerId = customers[0].Id, Date = DateTime.Now, ProductId = products[0].Id, Quantity = 5, OrderNumber= rnd.Next(1000, 9999) });
            co.Add(new CustomerOrder { Id = Guid.NewGuid(), CustomerId = customers[1].Id, Date = DateTime.Now, ProductId = products[0].Id, Quantity = 15, OrderNumber = rnd.Next(1000, 9999) });
            co.Add(new CustomerOrder { Id = Guid.NewGuid(), CustomerId = customers[0].Id, Date = DateTime.Now, ProductId = products[0].Id, Quantity = 5, OrderNumber = rnd.Next(1000, 9999) });
            context.CustomerOrders.AddRange(co);

            base.Seed(context);
        }
    }

    public class Entity
    {
        [HiddenInput]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [Display(Name = "Zip Code")]
        public string Zipcode { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:###-###-####}")]
        [Display(Name = "Phone Number")]
        public string Phonenumber { get; set; }
        [HiddenInput]
        public DateTime? AddedDate { get; set; }
        [HiddenInput]
        public DateTime? LastUpdated { get; set; }
        [Display(Name = "Active")]
        public bool Status { get; set; }
    }

    public class Customer : Entity
    {

    }

    public class Product
    {
        [HiddenInput]
        public Guid Id { get; set; }
        [Required]
        public string UPC { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Order Point")]
        public int OrderPoint { get; set; }
        [Display(Name = "Qty On Hand")]
        public int OnHand { get; set; }
        [Required]
        [Display(Name = "Retail Price")]
        public decimal SalePrice { get; set; }
        [Display(Name = "Active")]
        public bool Status { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? LastUpdated { get; set; }

    }

    public class InventoryOrder
    {
        public int OrderNumber { get; set; }
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime? Date { get; set; }
    }

    public class CustomerOrder
    {
        public Guid Id { get; set; }
        public int OrderNumber { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime? Date { get; set; }
    }

}


