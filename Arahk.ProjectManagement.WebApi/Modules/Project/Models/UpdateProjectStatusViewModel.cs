
using Arahk.ProjectManagement.WebApi.Modules.Project.Entities;

namespace Arahk.ProjectManagement.WebApi.Modules.Project.Models;

public class UpdateProjectStatusViewModel : CreateProjectStatusViewModel
{
    public int Id { get; set; }

    internal void UpdateEntity(ProjectStatusEntity entity)
    {
        entity.Name = Name;
        entity.Order = Order;
    }
}