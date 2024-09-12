using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class RolePermission
    {
        [Key]
        public int RolePermissionId { get; set; }

        // Foreign key to IdentityRole
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public IdentityRole<int> Role { get; set; }  // Navigation property to IdentityRole

        public string Permission { get; set; } // CRUD permissions on entities, access to features
    }
}
