using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyInventory.Models;

namespace MyInventory.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            InventoryContext context = new InventoryContext();
            var customers = context.Customers;
            return View(customers);
        }

        public ActionResult Create()
        {
            Customer customer = new Customer();
            customer.Status = true;
            return View(customer);
        }

        [HttpPost]
        public ActionResult Create(Customer model)
        {
            using (var context = new InventoryContext())
            {
                model.Id = Guid.NewGuid();
                model.AddedDate = DateTime.Now;
                model.LastUpdated = DateTime.Now;
                context.Customers.Add(model);
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(Guid Id)
        {
            using (var context = new InventoryContext())
            {
                var customer = context.Customers.FirstOrDefault(s => s.Id == Id);
                return View(customer);
            }
        }

        [HttpPost]
        public ActionResult Edit(Customer model)
        {
            using (var context = new InventoryContext())
            {
                var original = context.Customers.Find(model.Id);

                if (original != null)
                {
                    model.LastUpdated = DateTime.Now;
                    context.Entry(original).CurrentValues.SetValues(model);
                    context.SaveChanges();
                }

            }

            return RedirectToAction("Index");
        }
    }
}