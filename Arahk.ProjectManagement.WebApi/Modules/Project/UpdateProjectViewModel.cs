
using Arahk.ProjectManagement.WebApi.Data;

namespace Arahk.ProjectManagement.WebApi.Modules.Project;

public class UpdateProjectViewModel : CreateProjectViewModel
{
    public int Id { get; set; }

    public string Status { get; set; } = string.Empty;

    internal async Task UpdateEntity(ProjectEntity entity, AppDbContext dbContext)
    {
        var firstStatus = await LookupProjectStatus(dbContext);

        entity.Name = Name;
        entity.Description = Description;
        entity.StartDate = StartDate;
        entity.EndDate = EndDate;
        entity.StatusId = firstStatus.Id;
        entity.Status = firstStatus;
    }
}