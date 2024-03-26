using Blog.Application.Abstractions.JWT;
using Blog.Application.Abstractions.Services;
using Blog.Domain.Entities.Identity;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.CQRS.Commands.User.SignIn
{
    public class SignInUserCommandHandler : IRequestHandler<SignInUserCommandRequest, SignInUserCommandResponse>
    {
        readonly UserManager<AppUser> _userManager;
        readonly ITokenHandler _tokenHandler;
        readonly IUserService _userService;
        private IValidator<SignInUserCommandRequest> _validator;

        public SignInUserCommandHandler(UserManager<AppUser> userManager, ITokenHandler tokenHandler, IValidator<SignInUserCommandRequest> validator, IUserService userService)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _validator = validator;
            _userService = userService;
        }

        public async Task<SignInUserCommandResponse> Handle(SignInUserCommandRequest request, CancellationToken cancellationToken)
        {
            string mesaj = "";
            if (await _userService.isUserNameAlreadyTakenAsync(request.kullaniciAdi))
            {
                return new SignInUserCommandResponse() { basariDurum = false, mesaj = "kullanıcı adı alınmış" };
            }
            ValidationResult validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                foreach (ValidationFailure error in validationResult.Errors)
                {
                    mesaj += $"* {error.ErrorMessage} <br>";
                }
                return new SignInUserCommandResponse() { basariDurum = false, mesaj = mesaj };
            }


            IdentityResult result = await _userManager.CreateAsync(new() { Id=Guid.NewGuid().ToString(),UserName=request.kullaniciAdi },request.parola);
            
            if (result.Succeeded)
            {
                AppUser user = await _userManager.FindByNameAsync(request.kullaniciAdi);
                Token token = _tokenHandler.createAccessToken(120, user);
                await _userService.updateRefreshToken(token.refreshToken, user, token.expiration);
                return new SignInUserCommandResponse() { basariDurum=true,mesaj="Hesap oluşturma işlemi başarılı.",token=token};
            }
            
            foreach(IdentityError error in result.Errors)
            {
                mesaj += $"{error.Description} \n";
            }
            return new SignInUserCommandResponse() { basariDurum = false, mesaj = mesaj };
        }
    }
}
