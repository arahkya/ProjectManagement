using System.ComponentModel.DataAnnotations;
using Arahk.ProjectManagement.WebApi.Modules.Project.Entities;

namespace Arahk.ProjectManagement.WebApi.Modules.Project.Models;

public class CreateProjectStatusViewModel
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Range(1, 100)]
    public int Order { get; set; } = 0;

    public ProjectStatusEntity ToEntity()
    {
        return new ProjectStatusEntity
        {
            Name = Name,
            Order = Order
        };
    }
}