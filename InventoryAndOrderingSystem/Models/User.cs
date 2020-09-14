using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAndOrderingSystem.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Username { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public Nullable<DateTime> DateCreated { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public Nullable<DateTime> DateUpdated { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Password { get; set; }
        [ForeignKey("Role")]
        [Required]
        public int RoleID { get; set; }

        public Role Role { get; set; }
        public List<Order> Orders { get; set; }
    }
}
