using System.ComponentModel.DataAnnotations;
using Arahk.ProjectManagement.WebApi.Data;
using Arahk.ProjectManagement.WebApi.Modules.Project.Entities;
using Microsoft.EntityFrameworkCore;

namespace Arahk.ProjectManagement.WebApi.Modules.Project.Models;

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

    protected async Task<ProjectStatusEntity> LookupProjectStatus(AppDbContext dbContext)
    {
        var firstStatus = await dbContext.ProjectStatuses.OrderBy(p => p.Order).FirstOrDefaultAsync() ?? throw new ArgumentException($"Status not found.");

        return firstStatus;
    }

    public async Task<ProjectEntity> ToEntity(AppDbContext dbContext)
    {
        var firstStatus = await LookupProjectStatus(dbContext);

        return new ProjectEntity
        {
            Name = Name,
            Description = Description,
            StartDate = StartDate,
            EndDate = EndDate,
            StatusId = firstStatus.Id,
            Status = firstStatus
        };
    }
}