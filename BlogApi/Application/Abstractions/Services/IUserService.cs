using Blog.Application.Abstractions.JWT;
using Blog.Domain.Entities.Contracts.Role;
using Blog.Domain.Entities.Contracts.User;
using Blog.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<AppRole> getUserById(string id);
        Task<bool> deleteUser(string userId);
        Task<List<GetAllUser>> getAllUser();
        Task<bool> updateRefreshToken(string refreshToken,AppUser user,DateTime accessTokenDate);
        Task<bool> assignRole(string userId,string roleName);
        Task<bool> isUserNameAlreadyTakenAsync(string userName);
        Task<bool> hasPermissionToAction(string userName,string code);
        Task<bool> isAdmin(string userName);
        Task<string> getRoleByUserName(string userName);
        Task<Token> refreshTokenLoginAsync(string refreshToken);
    }
}
