using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyInventory.Models;

namespace MyInventory.Functions
{
    public class UpdateInventory
    {
        private InventoryContext db = new InventoryContext();

        public UpdateInventory(CustomerOrder customerOrder)
        {
            int sumCO = 0;

            if (db.CustomerOrders.Count(co => co.ProductId == customerOrder.ProductId) > 0)
            {
                sumCO = db.CustomerOrders.Where(co => co.ProductId == customerOrder.ProductId).Sum(q => q.Quantity);
            }

            int sumIO = 0;

            if (db.InventoryOrders.Count(co => co.ProductId == customerOrder.ProductId) > 0)
            {
                sumIO = db.InventoryOrders.Where(co => co.ProductId == customerOrder.ProductId).Sum(q => q.Quantity);
            }

            Product product = db.Products.Find(customerOrder.ProductId);

            product.OnHand = sumIO - sumCO;

            db.SaveChanges();

        }

        public UpdateInventory(InventoryOrder inventoryOrder)
        {
            int sumCO = 0;

            if (db.CustomerOrders.Count(co => co.ProductId == inventoryOrder.ProductId) > 0)
            {
                sumCO = db.CustomerOrders.Where(co => co.ProductId == inventoryOrder.ProductId).Sum(q => q.Quantity);
            }

            int sumIO = 0;

            if (db.InventoryOrders.Count(co => co.ProductId == inventoryOrder.ProductId) > 0)
            {
                sumIO = db.InventoryOrders.Where(co => co.ProductId == inventoryOrder.ProductId).Sum(q => q.Quantity);
            }
                    

            Product product = db.Products.Find(inventoryOrder.ProductId);

            product.OnHand = sumIO - sumCO;

            db.SaveChanges();


        }
    }
}