
namespace Arahk.ProjectManagement.WebApi.Modules.Project;

public class UpdateProjectStatusViewModel : CreateProjectStatusViewModel
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Order { get; set; }

    internal void UpdateEntity(ProjectStatusEntity entity)
    {
        entity.Name = Name;
        entity.Order = Order;
    }
}