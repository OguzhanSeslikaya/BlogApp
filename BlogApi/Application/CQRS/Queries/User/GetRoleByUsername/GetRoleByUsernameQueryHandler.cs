using Blog.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.CQRS.Queries.User.GetRoleByUsername
{
    public class GetRoleByUsernameQueryHandler : IRequestHandler<GetRoleByUsernameQueryRequest, GetRoleByUsernameQueryResponse>
    {
        readonly IUserService _userService;

        public GetRoleByUsernameQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<GetRoleByUsernameQueryResponse> Handle(GetRoleByUsernameQueryRequest request, CancellationToken cancellationToken)
        {
            string roleName = await _userService.getRoleByUserName(request.userName);
            return new GetRoleByUsernameQueryResponse() { roleName = roleName};
        }
    }
}
