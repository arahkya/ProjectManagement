using Arahk.ProjectManagement.WebApi.Modules.Project.Entities;
using Arahk.ProjectManager.WebApi;

namespace Arahk.ProjectManagement.WebApi.Tests;

public class ProjectStatusUnitTest(TestApplicationFactory<Program> factory) : IClassFixture<TestApplicationFactory<Program>>
{
    private readonly TestApplicationFactory<Program> _factory = factory;

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
        Assert.Equal(5, projectStastuses.Count());
    }
}