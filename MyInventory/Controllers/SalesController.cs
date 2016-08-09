using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyInventory.Models;
using MyInventory.Functions;

namespace MyInventory.Controllers
{
    public class SalesController : Controller
    {
        private InventoryContext db = new InventoryContext();

        // GET: Orders
        public ActionResult Index()
        {
            var col = from customer in db.Customers
                      join co in db.CustomerOrders on customer.Id equals co.CustomerId
                      join product in db.Products on co.ProductId equals product.Id
                      select new CustomerOrderListViewModel { Customer = customer.Name, Description = product.Description, OrderNumber = co.OrderNumber, Quantity = co.Quantity, Date = co.Date, OrderId = co.Id };

            return View(col);
        }


        // GET: Orders/Create
        public ActionResult Create()
        {
            CustomerOrderViewModel covm = new CustomerOrderViewModel();
            covm.Customers = db.Customers.ToList();
            covm.Products = db.Products.ToList();

            return View(covm);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrderNumber,CustomerId,ProductId,Quantity,Date")] CustomerOrder customerOrder)
        {

            Random rnd = new Random();
            customerOrder.OrderNumber = rnd.Next(1000, 9999);
            customerOrder.Id = Guid.NewGuid();
            customerOrder.Date = DateTime.Now;
            db.CustomerOrders.Add(customerOrder);
            db.SaveChanges();
            UpdateInventory ui = new UpdateInventory(customerOrder);
            return RedirectToAction("Index");

        }

        private void UpdateQuantityOnHand(CustomerOrder customerOrder)
        {

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
