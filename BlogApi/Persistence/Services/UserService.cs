using Blog.Application.Abstractions.JWT;
using Blog.Application.Abstractions.Services;
using Blog.Application.Repositories.Authorize.Endpoint;
using Blog.Domain.Entities.Contracts.Role;
using Blog.Domain.Entities.Contracts.User;
using Blog.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Persistence.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> _userManager;
        readonly ITokenHandler _tokenHandler;
        readonly IEndpointReadRepository _endpointReadRepository;

        public UserService(UserManager<AppUser> userManager, IEndpointReadRepository endpointReadRepository, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _endpointReadRepository = endpointReadRepository;
            _tokenHandler = tokenHandler;
        }

        public async Task<bool> assignRole(string userId,string roleName)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if(user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user,userRoles);
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }

        public async Task<bool> deleteUser(string userId)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                return result.Succeeded;
            }
            return false;
        }

        public async Task<List<GetAllUser>> getAllUser()
        {
            var users = await _userManager.Users.ToListAsync();
            List<GetAllUser> user = new List<GetAllUser>();
            foreach (var item in users)
            {
                IList<string> roles = await _userManager.GetRolesAsync(item);
                string roleName = "";
                if (roles != null)
                {
                    roleName = roles.FirstOrDefault();
                }
                if (item.isAdmin)
                {
                    roleName = "admin";
                }
                GetAllUser user1 = new GetAllUser()
                {
                    id = item.Id,
                    name = item.UserName,
                    role = roleName,
                };
                user.Add(user1);
            }

            return user;
        }

        public async Task<string?> getRoleByUserName(string userName)
        {
            AppUser user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                var list = await _userManager.GetRolesAsync(user);
                var role = list.FirstOrDefault();
                if (user.isAdmin)
                {
                    return "admin";
                }
                return role;
            }
            return null;
        }

        public Task<AppRole> getUserById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> hasPermissionToAction(string userName, string code)
        {
            AppUser user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var role = roles.First();
                if (role != null)
                {
                    var endpoint = await _endpointReadRepository.table.Include(e => e.roles).FirstOrDefaultAsync(e => e.code == code);
                    if(endpoint != null)
                    {
                        foreach (var item in endpoint.roles)
                        {
                            if (item.Name == role)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public async Task<bool> isAdmin(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if(user != null)
            {
                return user.isAdmin;
            }
            return false;
        }

        public async Task<bool> isUserNameAlreadyTakenAsync(string userName)
        {
            AppUser user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return false;
            }
            return true;
        }

        public async Task<Token> refreshTokenLoginAsync(string refreshToken)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.refreshToken == refreshToken);
            if (user != null && user?.refreshTokenEndDate > DateTime.UtcNow)
            {
                Token token = _tokenHandler.createAccessToken(1, user);
                await updateRefreshToken(token.refreshToken,user,token.expiration);
                return token;
            }
            return null;
        }

        public async Task<bool> updateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate)
        {
            user.refreshToken = refreshToken;
            user.refreshTokenEndDate = accessTokenDate.AddMinutes(30);
            IdentityResult result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }
    }
}
