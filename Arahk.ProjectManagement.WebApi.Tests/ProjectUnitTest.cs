using Arahk.ProjectManagement.WebApi.Modules.Project.Entities;
using Arahk.ProjectManager.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;

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
}