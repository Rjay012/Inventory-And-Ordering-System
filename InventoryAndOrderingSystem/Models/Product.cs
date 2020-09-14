using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAndOrderingSystem.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string ProductName { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int Quantity { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime DateCreated { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime DateUpdated { get; set; }
        [Column(TypeName = "varchar(max)")]
        public string Description { get; set; }

        public List<Order> Orders { get; set; }
    }
}
