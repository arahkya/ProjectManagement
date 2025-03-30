using Microsoft.AspNetCore.Mvc;
using Arahk.ProjectManagement.WebApi.Data;
using Arahk.ProjectManagement.WebApi.Modules.Project;
using Microsoft.EntityFrameworkCore;

namespace Arahk.ProjectManagerment.WebApi.Modules.Project;

[ApiController]
[Route("[controller]")]
public class ProjectController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProjectViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var entity = model.ToEntity();

        await _context.Projects.AddAsync(entity);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Create), new { id = entity.Id }, entity);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var entity = await _context.Projects.FindAsync(id);
        if (entity == null)
        {
            return NotFound();
        }

        return Ok(entity);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var projects = await _context.Projects.ToListAsync();

        return Ok(projects);
    }

    [HttpPatch]
    public async Task<IActionResult> Update([FromBody] UpdateProjectViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var entity = await _context.Projects.FindAsync(model.Id);
        if (entity == null)
        {
            return NotFound();
        }

        model.UpdateEntity(entity);

        _context.Projects.Update(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _context.Projects.FindAsync(id);
        if (entity == null)
        {
            return NotFound();
        }

        _context.Projects.Remove(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}