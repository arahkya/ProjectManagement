using Arahk.ProjectManagement.WebApi.Modules.Project.Entities;
using Microsoft.EntityFrameworkCore;

namespace Arahk.ProjectManagement.WebApi.Data;

public class AppDbContext : DbContext
{
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<ProjectStatusEntity> ProjectStatuses { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // Configure your entities here
    }
}