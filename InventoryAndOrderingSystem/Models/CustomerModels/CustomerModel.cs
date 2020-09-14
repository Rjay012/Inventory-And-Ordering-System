using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAndOrderingSystem.Models.CustomerModels
{
    public class CustomerModel
    {
        public int CustomerID { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
