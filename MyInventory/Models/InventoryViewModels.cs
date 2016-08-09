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

    public class CustomerOrderListViewModel
    {
        public Guid OrderId { get; set; }
        [Display(Name = "Order Number")]
        public int OrderNumber { get; set; }
        public string Customer { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public DateTime? Date { get; set; }
    }

    public class CustomerOrderViewModel
    {
        public List<Product> Products { get; set; }
        public List<Customer> Customers { get; set; }
        public CustomerOrder CustomerOrder { get; set; }
    }

    public class InventoryOrderListViewModel
    {
        [Display(Name = "Order Number")]
        public int OrderNumber { get; set; }
        public Guid OrderId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        [Display(Name = "Unit Cost")]
        public decimal UnitCost { get; set; }
        public DateTime? Date { get; set; }
    }

    public class InventoryOrderViewModel
    {
        public List<Product> Products { get; set; }
        public InventoryOrder InventoryOrder { get; set; }
    }

    public class CustomerReturnListViewModel
    {
        [Display(Name = "Order Number")]
        public int OrderNumber { get; set; }
        public string Customer { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public DateTime? Date { get; set; }
    }
}