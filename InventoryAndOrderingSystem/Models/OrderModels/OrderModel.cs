using InventoryAndOrderingSystem.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAndOrderingSystem.Models.OrderModels
{
    public class OrderModel : ProductModel
    {
        public int OrderID { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string ShippingAddress { get; set; }
        public int TotalQuantity { get; set; }
        public string CustomerName { get; set; }
        public string Status { get; set; }
        public int UserID { get; set; }
    }
}
