using BlazorControlCefa.Application.Constants;
using BlazorControlCefa.Data.Enums;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BlazorControlCefa.Data.Seeders.Identity
{
    public static class DefaultModeratorUser
    {
        public static async Task SeedAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new IdentityUser
            {
                UserName = "moderator@cuestamoras.com",
                Email = "moderator@cuestamoras.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Sqlserver2005.");
                    //await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Moderator.ToString());

                    var role = await roleManager.FindByNameAsync("Moderator");
                    await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Person.Create));
                    await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Person.Edit));
                    await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Person.View));
                    await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Person.Special));
                }               
            }
        }
    }
}
