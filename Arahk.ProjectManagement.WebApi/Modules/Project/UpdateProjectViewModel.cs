
namespace Arahk.ProjectManagement.WebApi.Modules.Project;

public class UpdateProjectViewModel : CreateProjectViewModel
{
    public int Id { get; set; }

    public string Status { get; set; } = string.Empty;

    internal void UpdateEntity(ProjectEntity entity)
    {
        entity.Name = Name;
        entity.Description = Description;
        entity.StartDate = StartDate;
        entity.EndDate = EndDate;
        entity.Status = Status;
    }
}