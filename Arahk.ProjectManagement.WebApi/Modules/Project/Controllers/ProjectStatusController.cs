using Microsoft.AspNetCore.Mvc;
using Arahk.ProjectManagement.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using Arahk.ProjectManagement.WebApi.Modules.Project.Models;

namespace Arahk.ProjectManagerment.WebApi.Modules.Project.Controllers;

[ApiController]
[Route("project/status")]
public class ProjectStatusController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProjectStatusViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var entity = model.ToEntity();

        await _context.ProjectStatuses.AddAsync(entity);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Create), new { id = entity.Id }, entity);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var entity = await _context.ProjectStatuses.FindAsync(id);
        if (entity == null)
        {
            return NotFound();
        }

        return Ok(entity);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var projects = await _context.ProjectStatuses.ToListAsync();

        return Ok(projects);
    }

    [HttpPatch]
    public async Task<IActionResult> Update([FromBody] UpdateProjectStatusViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var entity = await _context.ProjectStatuses.FindAsync(model.Id);
        if (entity == null)
        {
            return NotFound();
        }

        model.UpdateEntity(entity);

        _context.ProjectStatuses.Update(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _context.ProjectStatuses.FindAsync(id);
        if (entity == null)
        {
            return NotFound();
        }

        _context.ProjectStatuses.Remove(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}