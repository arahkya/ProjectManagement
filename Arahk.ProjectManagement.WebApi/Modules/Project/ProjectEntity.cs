using Arahk.ProjectManagement.WebApi.Modules.Project;

public class ProjectEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int StatusId { get; set; }
    public ProjectStatusEntity Status { get; set; }
}