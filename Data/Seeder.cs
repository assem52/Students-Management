using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudentManagerAPI.API.Services;
using StudentManagerAPI.Data;
using StudentManagerAPI.Entities;

namespace StudentManagerAPI.API.Seeding
{
    public static class Seeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;
            
            var roleService = serviceProvider.GetRequiredService<IRoleService>();
            await roleService.EnsureRolesExistAsync(new[] { "Admin", "Instructor", "Student" });

            var userManager = services.GetRequiredService<UserManager<User>>();
            var dbContext = services.GetRequiredService<AppDbContext>();

            var adminEmail = "admin@studentmanager.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            // Ensure Department exists
            var defaultDepartment = await dbContext.Departments.FirstOrDefaultAsync();
            if (defaultDepartment == null)
            {
                defaultDepartment = new Department { Name = "Administration" };
                dbContext.Departments.Add(defaultDepartment);
                await dbContext.SaveChangesAsync();
            }

            // Seed Admin user
            if (adminUser == null)
            {
                var admin = new User
                {
                    UserName = "admin",
                    Email = adminEmail,
                    Name = "System Administrator",
                    EmailConfirmed = true,
                    DepartmentId = defaultDepartment.Id
                };

                var createResult = await userManager.CreateAsync(admin, "Admin@123");
                if (createResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                    Console.WriteLine("✅ Admin user created successfully!");
                    Console.WriteLine($"Email: {adminEmail}, Password: Admin@123");
                }
                else
                {
                    Console.WriteLine("⚠️ Failed to create admin user:");
                    foreach (var error in createResult.Errors)
                        Console.WriteLine($"  - {error.Description}");
                }
            }
        }
    }
}
