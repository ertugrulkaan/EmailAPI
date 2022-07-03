using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace EmailAPI.Models.Identity
{
    public static class ApplicationDbContextSeed
    {
        public static void SeedData(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }
        private static void SeedRoles(RoleManager<ApplicationRole> roleManager)
        {
            //Seed roles
            if (!roleManager.RoleExistsAsync(Authorization.Roles.Administrator.ToString()).Result)
            {
                var role = new ApplicationRole() { Name = Authorization.Roles.Administrator.ToString(), NormalizedName = Authorization.Roles.Administrator.ToString() };
                roleManager.CreateAsync(role).Wait();
            }

            if (!roleManager.RoleExistsAsync(Authorization.Roles.User.ToString()).Result)
            {
                var role = new ApplicationRole() { Name = Authorization.Roles.User.ToString(), NormalizedName = Authorization.Roles.User.ToString() };
                roleManager.CreateAsync(role).Wait();
            }
        }
        private static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByNameAsync(Authorization.default_username).Result == null)
            {
                var defaultUser = new ApplicationUser
                {
                    UserName = Authorization.default_username,
                    Email = Authorization.default_email,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    FirstName = Authorization.default_firstname,
                    LastName = Authorization.default_lastname
                };

                var password = Authorization.default_password;
                var passwordHasher = new PasswordHasher<ApplicationUser>();
                defaultUser.PasswordHash = passwordHasher.HashPassword(defaultUser, password);

                var result = userManager.CreateAsync(defaultUser).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(defaultUser, Authorization.default_role.ToString()).Wait();
                }
            }
        }
    }
}
