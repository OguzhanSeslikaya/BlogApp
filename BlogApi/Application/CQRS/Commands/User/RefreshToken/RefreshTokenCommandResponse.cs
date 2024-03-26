using Blog.Application.Abstractions.JWT;

namespace Blog.Application.CQRS.Commands.User.RefreshToken
{
    public class RefreshTokenCommandResponse
    {
        public Token token { get; set; }
    }
}