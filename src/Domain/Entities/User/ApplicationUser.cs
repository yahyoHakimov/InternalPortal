using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.User
{
    public class ApplicationUser : IdentityUser<int>
    {
        public bool IsApproved { get; set; } = false;  // Indicates if HR has approved the user
        public int? EmployeeId { get; set; }  // Links to Employee entity once HR approves

    }
}
