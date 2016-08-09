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
    public class OrdersController : Controller
    {
        private InventoryContext db = new InventoryContext();

        // GET: Orders
        public ActionResult Index()
        {
            var col = from io in db.InventoryOrders
                      join product in db.Products on io.ProductId equals product.Id
                      select new InventoryOrderListViewModel { Description = product.Description, Quantity = io.Quantity, Date = io.Date, OrderNumber = io.OrderNumber };

            return View(col);
        }


        // GET: Orders/Create
        public ActionResult Create()
        {
            InventoryOrderViewModel iovm = new InventoryOrderViewModel();
            iovm.Products = db.Products.ToList();

            return View(iovm);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrderNumber,ProductId,Quantity,UnitCost,Date")] InventoryOrder inventoryOrder)
        {

            Random rnd = new Random();
            inventoryOrder.OrderNumber = rnd.Next(1000, 9999);
            inventoryOrder.Id = Guid.NewGuid();
            inventoryOrder.Date = DateTime.Now;
            db.InventoryOrders.Add(inventoryOrder);
            db.SaveChanges();
            UpdateInventory ui = new UpdateInventory(inventoryOrder);
            return RedirectToAction("Index");
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
