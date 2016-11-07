using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Giffy.DataAccess;
using Giffy.DataAccess.Infrastructure.Identity;
using Giffy.Entities.Models;
using Giffy.DataAccess.Models;

namespace Giffy.DataAccess.Repositories
{
    public class AuthRepository : IDisposable
    {
        private GiffyIdentityContext _ctx;

        private GiffyUserManager _userManager;

        private GiffyRoleManager _roleManager;

        public AuthRepository()
        {
            _ctx = new GiffyIdentityContext();
            _userManager = new GiffyUserManager(new UserStore<User>(_ctx));
            _roleManager = new GiffyRoleManager(new RoleStore<IdentityRole>(_ctx));
        }

        public async Task<IdentityResult> RegisterUser(RegisterUserDTO userModel)
        {
            Random random = new Random();

            User user = new User
            {
                UserName = userModel.UserName,
                Email = userModel.Email,
                JoinedDate = DateTime.UtcNow,
                LastLogin = DateTime.UtcNow,
                Gender = Entities.Models.Enums.Gender.Others,
                Avatar = string.Format("wwwroot/img/profile-pics/{0}.jpg", random.Next(1, 9))
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public async Task<User> FindUser(string userName, string password)
        {
            User user = await _userManager.FindAsync(userName, password);
            
            return user;
        }

        public Client FindClient(string clientId)
        {
            var client = _ctx.Clients.Find(clientId);

            return client;
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {

            var existingToken = _ctx.RefreshTokens.Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId).SingleOrDefault();

            if (existingToken != null)
            {
                var result = await RemoveRefreshToken(existingToken);
            }

            _ctx.RefreshTokens.Add(token);

            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

            if (refreshToken != null)
            {
                _ctx.RefreshTokens.Remove(refreshToken);
                return await _ctx.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            _ctx.RefreshTokens.Remove(refreshToken);
            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

            return refreshToken;
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
            return _ctx.RefreshTokens.ToList();
        }

        public async Task<User> FindAsync(UserLoginInfo loginInfo)
        {
            User user = await _userManager.FindAsync(loginInfo);

            return user;
        }

        public async Task<IdentityResult> CreateAsync(User user)
        {
            Random random = new Random();
            user.JoinedDate = DateTime.UtcNow;
            user.Avatar = string.Format("wwwroot/img/profile-pics/{0}.jpg", random.Next(1, 9));
            var result = await _userManager.CreateAsync(user);

            return result;
        }

        public async Task<IdentityResult> UpdateProfileAsync(User userInfo)
        {
           return await _userManager.UpdateAsync(userInfo);
        }

        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            var result = await _userManager.AddLoginAsync(userId, login);

            return result;
        }

        public IEnumerable<User> GetUsers()
        {
            var result = _userManager.Users.ToList();

            return result;
        }

        public async Task<User> GetUser(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);

            return user;
        }

        public async Task<User> GetUserByName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            return user;
        }

        public string GetUserRoles(User currentUser)
        {
            List<IdentityRole> lstRoles = _roleManager.Roles.ToList();

            return string.Join(";",
                lstRoles.Where(r => currentUser.Roles.Any(ur => ur.RoleId == r.Id))
                .Select(r => r.Name)
                .ToList());
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();
            _roleManager.Dispose();
        }
    }
}
