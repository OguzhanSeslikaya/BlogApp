using MediatR;

namespace Blog.Application.CQRS.Commands.User.AssignRole
{
    public class AssignRoleToUserCommandRequest : IRequest<AssignRoleToUserCommandResponse>
    {
        public string userId { get; set; }
        public string roleName { get; set; }
    }
}