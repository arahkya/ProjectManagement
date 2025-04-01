using Arahk.ProjectManagement.WebApi.Modules.Project.Entities;
using Arahk.ProjectManagement.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;
using Arahk.ProjectManagement.WebApi.Modules.Project.Models;
using System.Net;

namespace Arahk.ProjectManagement.WebApi.Tests;

public class ProjectUnitTest(TestApplicationFactory<Program> factory) : IClassFixture<TestApplicationFactory<Program>>
{
    private readonly TestApplicationFactory<Program> _factory = factory;

    [Fact]
    public async Task TestGetAllProject()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/project");
        var projects = await response.Content.ReadFromJsonAsync<IEnumerable<ProjectEntity>>();

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.NotNull(projects);
        Assert.Equal(15, projects.Count());
    }

    [Fact]
    public async Task TestCreateProject()
    {
        // Arrange
        var client = _factory.CreateClient();
        var newProject = new CreateProjectViewModel
        {
            Name = "New Project",
            Description = "This is a new project",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(30)
        };

        // Act
        var response = await client.PostAsJsonAsync("/project", newProject);
        var createdProject = await response.Content.ReadFromJsonAsync<ProjectEntity>();

        // Assert
        response.EnsureSuccessStatusCode();

        Assert.NotNull(createdProject);
        Assert.True(createdProject.Id > 0);
        Assert.True(createdProject.StatusId > 0);
        Assert.Equal("Not Started", createdProject.Status.Name);
    }

    [Fact]
    public async Task TestUpdateProject()
    {
        // Arrange
        var client = _factory.CreateClient();
        var projectId = 1; // Assuming a project with ID 1 exists
        var updatedProject = new UpdateProjectViewModel
        {
            Id = projectId,
            Name = "Updated Project",
            Description = "This is an updated project",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(60),
            StatusId = 2
        };

        // Act
        var response = await client.PatchAsJsonAsync($"/project", updatedProject);

        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task TestDeleteProject()
    {
        // Arrange
        var client = _factory.CreateClient();
        var projectId = 1; // Assuming a project with ID 1 exists

        // Act
        var response = await client.DeleteAsync($"/project/{projectId}");

        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task TestGetProjectById()
    {
        // Arrange
        var client = _factory.CreateClient();
        var projectId = 2; // Assuming a project with ID 1 exists

        // Act
        var response = await client.GetAsync($"/project/{projectId}");
        var project = await response.Content.ReadFromJsonAsync<ProjectEntity>();

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.NotNull(project);
        Assert.Equal(projectId, project.Id);
    }

    [Fact]
    public async Task TestGetAllProjects()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/project");
        var projects = await response.Content.ReadFromJsonAsync<IEnumerable<ProjectEntity>>();

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.NotNull(projects);
        Assert.Equal(15, projects.Count());
    }

    [Theory]
    [InlineData("", "This is a new project")]
    [InlineData("Another Project", "")]
    public async Task TestCreateProjectWithInvalidContext(string name, string desc)
    {
        // Arrange
        var client = _factory.CreateClient();
        var newProject = new CreateProjectViewModel
        {
            Name = name,
            Description = desc,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(30)
        };

        // Act
        var response = await client.PostAsJsonAsync("/project", newProject);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}