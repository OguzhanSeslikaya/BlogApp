using Blog.Application.Abstractions.Configurations;
using Blog.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.CQRS.Queries.Authorize.GetAuthorizeDefinitionEndPoint
{
    public class GetAuthorizeDefinitionEndPointQueryHandler : IRequestHandler<GetAuthorizeDefinitionEndPointQueryRequest, GetAuthorizeDefinitionEndPointResponse>
    {
        readonly IApplicationService _applicationService;
        readonly RoleManager<AppRole> _roleManager;

        public GetAuthorizeDefinitionEndPointQueryHandler(IApplicationService applicationService, RoleManager<AppRole> roleManager)
        {
            _applicationService = applicationService;
            _roleManager = roleManager;
        }

        public async Task<GetAuthorizeDefinitionEndPointResponse> Handle(GetAuthorizeDefinitionEndPointQueryRequest request, CancellationToken cancellationToken)
        {

            var menus = _applicationService.getAuthorizeDefinitionEndPoints(request.type);
            AppRole? role = await _roleManager.Roles.Where(r => r.Id.Equals(request.id)).Include(r=>r.endpoints).FirstOrDefaultAsync();
            if(role != null)
            {
                List<string> codes= role.endpoints.Select(e => e.code).ToList();
                return new GetAuthorizeDefinitionEndPointResponse() { authorizeEndpoints = codes, menus=menus };
            }
            return new GetAuthorizeDefinitionEndPointResponse() {menus = menus };
        }
    }
}
