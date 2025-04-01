using Arahk.ProjectManagement.WebApi.Modules.Project.Entities;
using Arahk.ProjectManagement.WebApi;
using Arahk.ProjectManagement.WebApi.Modules.Project.Models;

namespace Arahk.ProjectManagement.WebApi.Tests;

public class ProjectStatusUnitTest(TestApplicationFactory<Program> factory) : IClassFixture<TestApplicationFactory<Program>>
{
    private readonly TestApplicationFactory<Program> _factory = factory;

    [Fact]
    public async Task TestGetProjectStatusById()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/project/status/1");
        var projectStatus = await response.Content.ReadFromJsonAsync<ProjectStatusEntity>();

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.NotNull(projectStatus);
        Assert.Equal(1, projectStatus.Id);
        Assert.Equal("Not Started", projectStatus.Name);
        Assert.Equal(1, projectStatus.Order);
    }

    [Fact]
    public async Task TestGetAllProjectStatus()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/project/status");
        var projectStastuses = await response.Content.ReadFromJsonAsync<IEnumerable<ProjectStatusEntity>>();

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.NotNull(projectStastuses);
        Assert.True(projectStastuses.Count() > 0);
    }

    [Fact]
    public async Task TestDeleteProjectStatus()
    {
        // Arrange
        var client = _factory.CreateClient();
        var newProjectStatus = new CreateProjectStatusViewModel
        {
            Name = "Paused",
            Order = 9
        };

        var response = await client.PostAsJsonAsync("/project/status", newProjectStatus);
        var createdProjectStatus = await response.Content.ReadFromJsonAsync<ProjectStatusEntity>();

        // Act
        response = await client.DeleteAsync($"/project/status/{createdProjectStatus!.Id}");

        // Assert
        response.EnsureSuccessStatusCode();
    }


    [Fact]
    public async Task TestCreateProjectStatus()
    {
        // Arrange
        var client = _factory.CreateClient();
        var newProjectStatus = new CreateProjectStatusViewModel
        {
            Name = "Paused",
            Order = 9
        };

        // Act
        var response = await client.PostAsJsonAsync("/project/status", newProjectStatus);
        var createdProjectStatus = await response.Content.ReadFromJsonAsync<ProjectStatusEntity>();

        // Assert
        response.EnsureSuccessStatusCode();

        Assert.NotNull(createdProjectStatus);
        Assert.True(createdProjectStatus.Id > 0);
    }

    [Fact]
    public async Task TestUpdateProjectStatus()
    {
        // Arrange
        var client = _factory.CreateClient();
        var newProjectStatus = new CreateProjectStatusViewModel
        {
            Name = "Paused",
            Order = 9
        };

        var response = await client.PostAsJsonAsync("/project/status", newProjectStatus);
        var createdProjectStatus = await response.Content.ReadFromJsonAsync<ProjectStatusEntity>();

        // Act
        var updateProjectStatus = new UpdateProjectStatusViewModel
        {
            Id = createdProjectStatus!.Id,
            Name = "In Progress",
            Order = 8
        };

        response = await client.PatchAsJsonAsync("/project/status", updateProjectStatus);

        // Assert
        response.EnsureSuccessStatusCode();
    }
}