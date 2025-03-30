using System.ComponentModel.DataAnnotations;

namespace Arahk.ProjectManagement.WebApi.Modules.Project;

public class CreateProjectViewModel
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    public DateTime StartDate { get; set; } = DateTime.MinValue.Date;

    public DateTime EndDate { get; set; } = DateTime.MaxValue.Date;

    public ProjectEntity ToEntity()
    {
        return new ProjectEntity
        {
            Name = Name,
            Description = Description,
            StartDate = StartDate,
            EndDate = EndDate,
            Status = "New"
        };
    }
}