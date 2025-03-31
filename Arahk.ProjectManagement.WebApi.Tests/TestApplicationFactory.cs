using Arahk.ProjectManagement.WebApi.Data;
using Arahk.ProjectManagement.WebApi.Modules.Project.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;

namespace Arahk.ProjectManagement.WebApi.Tests;

public class TestApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        builder.ConfigureServices(services =>
        {
            // Remove the existing DbContext registration
            var descriptors = new List<ServiceDescriptor>(services.Where(
                d => d.ServiceType == typeof(DbContextOptions<AppDbContext>)
                || d.ServiceType == typeof(AppDbContext)
                || d.ServiceType.FullName!.Contains("Microsoft.EntityFrameworkCore")));

            foreach (var descriptor in descriptors)
            {
                services.Remove(descriptor);
            }

            // Add any test-specific services or configurations here
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });

            SeedProjects();
        });
    }

    public void SeedProjects()
    {
        using (var scope = Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Seed the database with test data
            if (!dbContext.Projects.Any())
            {
                // Give me 15 ProjectEntity with random data
                var projects = new List<ProjectEntity>();
                for (int i = 0; i < 15; i++)
                {
                    var startDate = DateTime.UtcNow.AddDays(Random.Shared.Next(1, 30));
                    var endDate = startDate.AddDays(Random.Shared.Next(1, 120));
                    projects.Add(new ProjectEntity
                    {
                        Name = $"Project {i + 1}",
                        Description = $"Description for Project {i + 1}",
                        StartDate = startDate,
                        EndDate = endDate,
                        StatusId = Random.Shared.Next(1, 5)
                    });
                }

                dbContext.SaveChanges();
            }
        }
    }
}