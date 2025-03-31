
using Arahk.ProjectManagement.WebApi.Data;
using Arahk.ProjectManagement.WebApi.Modules.Project.Entities;

namespace Arahk.ProjectManagement.WebApi.Modules.Project.Models;

public class UpdateProjectViewModel : CreateProjectViewModel
{
    public int Id { get; set; }

    public int StatusId { get; set; }

    internal async Task UpdateEntity(ProjectEntity entity, AppDbContext dbContext)
    {
        var status = await dbContext.ProjectStatuses.FindAsync(StatusId) ?? throw new ArgumentException($"Status not found.");

        entity.Name = Name;
        entity.Description = Description;
        entity.StartDate = StartDate;
        entity.EndDate = EndDate;
        entity.StatusId = status.Id;
        entity.Status = status;
    }
}