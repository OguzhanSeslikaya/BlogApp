using MediatR;

namespace Blog.Application.CQRS.Commands.User.RefreshToken
{
    public class RefreshTokenCommandRequest : IRequest<RefreshTokenCommandResponse>
    {
        public string refreshToken { get; set; }
    }
}