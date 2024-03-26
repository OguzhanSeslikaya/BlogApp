using MediatR;

namespace Blog.Application.CQRS.Commands.AuthorizationEndpoint.AssignRoleEndpoint
{
    public class AssignRoleEndpointCommandRequest : IRequest<AssignRoleEndpointCommandResponse>
    {
        public string roleId { get; set; }
        public string[] endPointsCode { get; set; }
        public Type? type { get; set; }
    }
}