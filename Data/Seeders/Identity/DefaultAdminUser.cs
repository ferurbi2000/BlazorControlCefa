using BlazorControlCefa.Application.Constants;
using BlazorControlCefa.Data.Enums;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BlazorControlCefa.Data.Seeders.Identity
{
    public static class DefaultAdminUser
    {
        public static async Task SeedAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new IdentityUser
            {
                UserName = "admin@cuestamoras.com",
                Email = "admin@cuestamoras.com",
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
                    //await userManager.AddToRoleAsync(defaultUser, Roles.Moderator.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());

                    await roleManager.SeedClaimsForAdmin();
                }                
            }
        }

        private async static Task SeedClaimsForAdmin(this RoleManager<IdentityRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("Admin");
            await roleManager.AddPermissionClaimForAdmin(adminRole, "Persons");
            //await roleManager.AddPermissionClaim(adminRole, "Locations");
            //await roleManager.AddPermissionClaim(adminRole, "InventoryTypes");
        }
        public static async Task AddPermissionClaimForAdmin(this RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permissions.GeneratePermissionsForModule(module);
            foreach (var permission in allPermissions)
            {
                if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
                {
                    await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, permission));
                }
            }
        }
    }
}
