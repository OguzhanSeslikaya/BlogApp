using Blog.Application.Abstractions.Hubs;
using Blog.Application.Abstractions.JWT;
using Blog.Application.Abstractions.Services;
using Blog.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.CQRS.Commands.User.LogIn
{
    public class LogInUserCommandHandler : IRequestHandler<LogInUserCommandRequest, LogInUserCommandResponse>
    {
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        readonly ITokenHandler _tokenHandler;
        readonly IUserService _userService;
        readonly IDailyLoginsHubService _dailyLoginsHubService;

        public LogInUserCommandHandler(UserManager<AppUser> userManager, ITokenHandler tokenHandler, SignInManager<AppUser> signInManager, IUserService userService, IDailyLoginsHubService dailyLoginsHubService)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _signInManager = signInManager;
            _userService = userService;
            _dailyLoginsHubService = dailyLoginsHubService;
        }

        public async Task<LogInUserCommandResponse> Handle(LogInUserCommandRequest request, CancellationToken cancellationToken)
        {
            AppUser user = await _userManager.FindByNameAsync(request.kullaniciAdi);
            if (user == null)
            {
                return new LogInUserCommandResponse() { basariDurum = false , mesaj = "Kullanıcı bulunamadı."};
            }
            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user,request.parola,false);
            if (result.Succeeded) 
            {
                Token token = _tokenHandler.createAccessToken(120, user);
                await _userService.updateRefreshToken(token.refreshToken,user,token.expiration);

                await _dailyLoginsHubService.newLoginMessageAsync(request.kullaniciAdi+" adlı kullanıcı giriş yaptı.");

                return new LogInUserCommandResponse() { basariDurum = true, mesaj = "Giriş Başarılı.", token = token};
            }
            return new LogInUserCommandResponse() {basariDurum=false,mesaj="Şifre Yanlış." };
        }
    }
}
