using Blog.Application.Abstractions.Services;
using Blog.Domain.Entities.Contracts.Role;
using Blog.Domain.Entities.Contracts.User;
using Blog.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Persistence.Services
{
    public class RoleService : IRoleService
    {
        readonly RoleManager<AppRole> _roleManager;
        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<AppRole> getRoleById(string id)
        {
            AppRole appRole = await _roleManager.FindByIdAsync(id);
            return appRole;
        }

        public async Task<bool> createRole(string name)
        {
            IdentityResult result = await _roleManager.CreateAsync(new() { Name = name });
            return result.Succeeded;
        }

        public async Task<bool> deleteRole(string name)
        {
            AppRole appRole = await _roleManager.FindByNameAsync(name);
            IdentityResult result = await _roleManager.DeleteAsync(appRole);
            return result.Succeeded;
        }

        public GetAllRole[] getAllRoles()
        {
            return _roleManager.Roles.Select(r => new GetAllRole(){id = r.Id,name = r.Name}).ToArray();
        }

        public async Task<bool> updateRole(string id,string name)
        {
            AppRole appRole = await _roleManager.FindByIdAsync(id);
            appRole.Name = name;
            IdentityResult result = await _roleManager.UpdateAsync(appRole);
            return result.Succeeded;
        }

    }
}
