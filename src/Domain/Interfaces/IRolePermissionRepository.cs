using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRolePermissionRepository
    {
        Task<IEnumerable<RolePermission>> GetAllRolePermissions();
        Task<RolePermission> GetRolePermissionById(int id);
        Task AddRolePermission(RolePermission rolePermission);
        Task UpdateRolePermission(RolePermission rolePermission);
        Task DeleteRolePermission(int id);
    }
}
