using EmailAPI.Core.Model;
using EmailAPI.Models;
using EmailAPI.Models.Identity;
using EmailAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmailAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<ApplicationUser> userManager;

        public AccountService(IConfiguration configuration,
                              UserManager<ApplicationUser> userManager)
        {
            this.configuration = configuration;
            this.userManager = userManager;
        }

        public async Task<string> RegisterAsync(RegisterModel user)
        {
            var userWithSameEmail = await userManager.FindByEmailAsync(user.Email);
            if (userWithSameEmail == null)
            {
                ApplicationUser userModel = new ApplicationUser 
                {
                    UserName = user.UserName,
                    Email = user.Email, 
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Address = user.Address,
                    Country = user.Country                    
                };
                var result = await userManager.CreateAsync(userModel, user.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(userModel, Authorization.Roles.User.ToString());
                }
                return $"User Registered {user.UserName}";
            }
            else
            {
                return $"Email {user.Email} is already registered.";
            }
        }
        public async Task<string> GenerateTokenAsync(ApplicationUser user, string key, string issuer)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var userClaims = await userManager.GetClaimsAsync(user);
            var roles = await userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("UserId", user.Id.ToString())
            };
            // Add roles as multiple claims
            claims.AddRange(roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));


            var token = new JwtSecurityToken(issuer,
                              issuer,
                              claims,
                              expires: DateTime.Now.AddMinutes(120),
                              signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        public async Task<ApplicationUser> GetUserAsync(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            return user;
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            return user;
        }


        public async Task<IdentityResult> ResetUserPassword(ApplicationUser user, string newPassword)
        {
            var resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
            var result = await userManager.ResetPasswordAsync(user, resetToken, newPassword);

            return result;
        }

        public async Task<ApplicationUser> ValidateUserAsync(string userName, string password)
        {
            var identityUser = await userManager.FindByNameAsync(userName);

            if (identityUser != null)
            {
                var result = await userManager.CheckPasswordAsync(identityUser, password);
                if (result)
                {
                    return identityUser;
                }
            }

            return null;
        }
    }
}
