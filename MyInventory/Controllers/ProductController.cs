using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyInventory.Models;

namespace MyInventory.Controllers
{
    public class ProductController : Controller
    {
        private InventoryContext db = new InventoryContext();

        // GET: Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }


        // GET: Products/Create
        public ActionResult Create()
        {
            var product = new Product();
            product.Status = true;

            return View(product);
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SupplierId,Description,OrderPoint,SalePrice,Status,AddedDate,UPC,LastUpdated")] Product product)
        {

            product.Id = Guid.NewGuid();
            product.AddedDate = DateTime.Now;
            product.LastUpdated = DateTime.Now;
            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Products/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SupplierId,Description,OrderPoint,OnHand,SalePrice,Status,AddedDate,LastUpdated")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.LastUpdated = DateTime.Now;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
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
