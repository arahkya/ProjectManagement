using Arahk.ProjectManagement.WebApi.Modules.Project.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arahk.ProjectManagement.WebApi.Modules.Project;

public class ProjectStatusEntityTypeConfigure : IEntityTypeConfiguration<ProjectStatusEntity>
{
    public void Configure(EntityTypeBuilder<ProjectStatusEntity> builder)
    {
        builder.ToTable("ProjectStatuses");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(50);
    }
}