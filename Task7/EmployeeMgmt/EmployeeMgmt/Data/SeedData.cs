using Microsoft.AspNetCore.Identity;

namespace EmployeeMgmt.Data
{
    public static class SeedData
    {
        public static async Task SeedRolesAndUsers(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string[] roles = { "Admin", "Manager", "Employee" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // Admin user
            var adminEmail = "admin@example.com";
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var admin = new IdentityUser { UserName = "admin", Email = adminEmail, EmailConfirmed = true };
                var res = await userManager.CreateAsync(admin, "Admin@123");
                if (res.Succeeded) await userManager.AddToRoleAsync(admin, "Admin");
            }

            // Manager user
            var managerEmail = "manager@example.com";
            if (await userManager.FindByEmailAsync(managerEmail) == null)
            {
                var manager = new IdentityUser { UserName = "manager", Email = managerEmail, EmailConfirmed = true };
                var res = await userManager.CreateAsync(manager, "Manager@123");
                if (res.Succeeded) await userManager.AddToRoleAsync(manager, "Manager");
            }

            // Employee user
            var empEmail = "employee@example.com";
            if (await userManager.FindByEmailAsync(empEmail) == null)
            {
                var emp = new IdentityUser { UserName = "employee", Email = empEmail, EmailConfirmed = true };
                var res = await userManager.CreateAsync(emp, "Employee@123");
                if (res.Succeeded) await userManager.AddToRoleAsync(emp, "Employee");
            }
        }
    }
}
