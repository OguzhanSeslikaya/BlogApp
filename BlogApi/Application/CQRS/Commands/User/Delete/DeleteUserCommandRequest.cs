using MediatR;

namespace Blog.Application.CQRS.Commands.User.Delete
{
    public class DeleteUserCommandRequest : IRequest<DeleteUserCommandResponse>
    {
        public string id { get; set; }
    }
}