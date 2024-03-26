using Blog.Domain.Entities.Contracts.Role;
using Blog.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Abstractions.Services
{
    public interface IRoleService
    {
        Task<AppRole> getRoleById(string id);
        Task<bool> createRole(string name);
        Task<bool> deleteRole(string name);
        Task<bool> updateRole(string id,string name);
        GetAllRole[] getAllRoles();

    }
}
