using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StudentManagerAPI.API.Extensions;
using StudentManagerAPI.API.Seeding;

namespace StudentManagerAPI;

class Program
{
    static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Service registrations
        builder.Services.AddDatabase(builder.Configuration)
            .AddIdentityConfiguration()
            .AddJwtAuthentication(builder.Configuration)
            .AddSwaggerWithAuth()
            .AddApplicationServices()
            .AddRepositories();

        builder.Services.AddControllers()
        .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

        var app = builder.Build();

        // Database Seeding (Roles + Admin + Default Department)
        await Seeder.SeedAsync(app.Services);

        // Middleware pipeline
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}