using Arahk.ProjectManagement.WebApi.Data;
using Arahk.ProjectManagement.WebApi.Modules.Project.Entities;
using Microsoft.EntityFrameworkCore;

namespace Arahk.ProjectManagement.WebApi.Modules.Project;

public static class ProjectStatusSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var projectStatuses = await appDbContext.ProjectStatuses.ToListAsync();

        if (projectStatuses.Count == 0)
        {
            var defaultProjectStatuses = new List<ProjectStatusEntity>
            {
                new () { Name = "Not Started", Order = 1 },
                new () { Name = "In Progress", Order = 2 },
                new () { Name = "Completed", Order = 3 },
                new () { Name = "On Hold", Order = 4 },
                new () { Name = "Cancelled", Order= 5 }
            };

            foreach (var status in defaultProjectStatuses)
            {
                await appDbContext.AddAsync(status);
            }

            await appDbContext.SaveChangesAsync();
        }
    }
}