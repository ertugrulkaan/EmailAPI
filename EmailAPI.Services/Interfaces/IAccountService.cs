using EmailAPI.Core.Model;
using EmailAPI.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace EmailAPI.Services.Interfaces
{
    public interface IAccountService
    {
        //Task<IdentityResult> CreateApplicationUserAsync(User user);
        Task<string> RegisterAsync(RegisterModel user);
        Task<ApplicationUser> GetUserAsync(string userName);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<ApplicationUser> ValidateUserAsync(string userName, string password);
        Task<string> GenerateTokenAsync(ApplicationUser user, string key, string issuer);
        Task<IdentityResult> ResetUserPassword(ApplicationUser user, string newPassword);
    }
}
