using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace EmailAPI.Models.Identity
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedEssentialAsync(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            //Seed roles
            await roleManager.CreateAsync(new ApplicationRole() { Name = Authorization.Roles.Administrator.ToString(), NormalizedName = Authorization.Roles.Administrator.ToString() });
            await roleManager.CreateAsync(new ApplicationRole() { Name = Authorization.Roles.User.ToString(), NormalizedName = Authorization.Roles.Administrator.ToString() });

            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = Authorization.default_username,
                Email = Authorization.default_email,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                FirstName = Authorization.default_firstname,
                LastName = Authorization.default_lastname
            };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                await userManager.CreateAsync(defaultUser, Authorization.default_password);
                await userManager.AddToRoleAsync(defaultUser, Authorization.default_role.ToString());
            }
        }
    }
}
