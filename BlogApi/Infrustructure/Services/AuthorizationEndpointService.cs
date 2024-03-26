using Blog.Application.Abstractions.Configurations;
using Blog.Application.Abstractions.Services;
using Blog.Application.Repositories.Authorize.Endpoint;
using Blog.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrustructure.Services
{
    internal class AuthorizationEndpointService : IAuthorizationEndpointService
    {
        readonly IApplicationService _applicationService;
        readonly IEndpointReadRepository _endpointReadRepository;
        readonly IEndpointWriteRepository _endpointWriteRepository;
        readonly IRoleService _roleService;
        readonly RoleManager<AppRole> _roleManager;


        public AuthorizationEndpointService(IApplicationService applicationService, IEndpointWriteRepository endpointWriteRepository, IEndpointReadRepository endpointReadRepository, IRoleService roleService, RoleManager<AppRole> roleManager)
        {
            _applicationService = applicationService;
            _endpointWriteRepository = endpointWriteRepository;
            _endpointReadRepository = endpointReadRepository;
            _roleService = roleService;
            _roleManager = roleManager;
        }

        public async Task<bool> AssignEndpointForRoleAsync(string roleId, string[] endPointCodes,Type type)
        {
            AppRole? role = await _roleManager.Roles.Where(r => r.Id.Equals(roleId)).Include(r => r.endpoints).FirstOrDefaultAsync();
            if (role != null)
            {
                role.endpoints.Clear();
                await _endpointWriteRepository.saveAsync();
                List<Menu> menus = _applicationService.getAuthorizeDefinitionEndPoints(type);
                foreach (var endpointCode in endPointCodes) {
                Blog.Domain.Entities.Identity.Endpoint? endpoint = await _endpointReadRepository.table.FirstOrDefaultAsync(e => e.code.Equals(endpointCode));
                foreach (var menu in menus)
                    {
                        foreach (var action in menu.actions)
                        {
                        
                        if (endpointCode.Equals(action.code))
                        {
                            if (endpoint == null)
                                {
                                var yeniEndpoint = new Blog.Domain.Entities.Identity.Endpoint() {code = endpointCode };
                                yeniEndpoint.roles.Add(role);
                                bool boolEndpoint = await _endpointWriteRepository.addAsync(yeniEndpoint);
                                }
                                else {
                                    endpoint.roles.Add(role);
                                }
                            
                            
                        }
                    }
                }
            }
                await _endpointWriteRepository.saveAsync();
            }
            
            return true;
    }
    }
}
