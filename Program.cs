using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using StudentManagerAPI.API.Extensions;
using StudentManagerAPI.API.Services;
using StudentManagerAPI.Data;
using StudentManagerAPI.Entities;
using User = StudentManagerAPI.Entities.User;
using System.Text;

namespace StudentManagerAPI;

class Program
{
    static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Configure database context
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Configure Identity
        builder.Services.ConfigureIdentity();

        // Configure JWT Auth
        var jwtSettings = builder.Configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["Secret"] ?? throw new InvalidOperationException("JWT Secret Key Not Configured");

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["ValidIssuer"],
                ValidAudience = jwtSettings["ValidAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };
        });

        // Register services
        builder.Services.ConfigureServices();
        builder.Services.ConfigureRepositoryManager();

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Ensure Roles and Admin User
        using (var scope = app.Services.CreateScope())
        {
            var roleService = scope.ServiceProvider.GetRequiredService<RoleService>();
            await roleService.EnsureRolesExistAsync(new[] { "Admin", "Instructor", "Student" });
            
            // Seed admin user
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var adminEmail = "admin@studentmanager.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            
            // Ensure there's at least one department
            var defaultDepartment = await dbContext.Departments.FirstOrDefaultAsync();
            if (defaultDepartment == null)
            {
                defaultDepartment = new Department
                {
                    Name = "Administration",
                    Students = new List<User>(),
                    Courses = new List<Course>()
                };
                dbContext.Departments.Add(defaultDepartment);
                await dbContext.SaveChangesAsync();
            }
            
            if (adminUser == null)
            {
                // Ensure the department exists and is tracked
                var department = await dbContext.Departments.FindAsync(defaultDepartment.Id);
                if (department == null)
                {
                    throw new InvalidOperationException("Default department not found after creation");
                }
                
                var admin = new User
                {
                    UserName = "admin",
                    Email = adminEmail,
                    Name = "System Administrator",
                    EmailConfirmed = true,
                    DepartmentId = department.Id,
                    Department = department
                };
                
                var createResult = await userManager.CreateAsync(admin, "Admin@123");
                if (createResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                    Console.WriteLine("Admin user created successfully!");
                    Console.WriteLine($"Email: {adminEmail}");
                    Console.WriteLine("Password: Admin@123");
                }
            }
        }
        
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
