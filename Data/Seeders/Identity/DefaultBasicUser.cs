using BlazorControlCefa.Application.Constants;
using BlazorControlCefa.Data.Enums;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BlazorControlCefa.Data.Seeders.Identity
{
    public static class DefaultBasicUser
    {
        public static async Task SeedAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new IdentityUser
            {
                UserName = "basic@cuestamoras.com",
                Email = "basic@cuestamoras.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Sqlserver2005.");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());

                    var role = await roleManager.FindByNameAsync("Basic");
                    await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Person.View));
                }               
            }
        }
    }
}
