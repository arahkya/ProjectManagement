namespace Arahk.ProjectManagement.WebApi.Modules.Project.Entities;

public class ProjectStatusEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int Order { get; set; }
}