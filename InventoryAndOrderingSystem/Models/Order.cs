using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAndOrderingSystem.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public Nullable<DateTime> DeliveryDate { get; set; }
        [Required]
        [Column(TypeName = "varchar(max)")]
        public string ShipingAddress { get; set; }
        [Required]
        [Column(TypeName = "varchar(20)")]
        public string Status { get; set; }

        public User User { get; set; }
        public Product Product { get; set; }
        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
