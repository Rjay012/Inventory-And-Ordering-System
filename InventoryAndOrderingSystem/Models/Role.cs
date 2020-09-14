using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAndOrderingSystem.Models
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string RoleName { get; set; }

        List<User> Users { get; set; }
    }
}
