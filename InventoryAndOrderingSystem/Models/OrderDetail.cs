using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAndOrderingSystem.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailID { get; set; }
        [ForeignKey("Order")]
        public int OrderID { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int Quantity { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public Order Order { get; set; }
    }
}
