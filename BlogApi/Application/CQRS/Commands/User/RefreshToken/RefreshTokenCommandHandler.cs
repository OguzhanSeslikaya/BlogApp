using Blog.Application.Abstractions.JWT;
using Blog.Application.Abstractions.Services;
using Blog.Application.CQRS.Commands.User.LogIn;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.CQRS.Commands.User.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommandRequest, RefreshTokenCommandResponse>
    {
        readonly IUserService _userService;

        public RefreshTokenCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
        {
             Token token = await _userService.refreshTokenLoginAsync(request.refreshToken);
            return new() { token = token }; 
        }
    }
}
