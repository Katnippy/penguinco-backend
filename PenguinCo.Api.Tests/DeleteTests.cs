using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.DependencyInjection;
using PenguinCo.Api.Data;
using PenguinCo.Api.Endpoints;

namespace PenguinCo.Api.Tests;

public class DeleteTests : IAsyncLifetime
{
    private readonly TestApp _app;
    private readonly PenguinCoContext _dbContext;

    public DeleteTests()
    {
        _app = new();
        var scope = _app.Services.CreateScope();
        _dbContext = scope.ServiceProvider.GetRequiredService<PenguinCoContext>();
    }

    public async Task InitializeAsync()
    {
        await _dbContext.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await _dbContext.Database.EnsureDeletedAsync();
    }

    [Fact]
    public async Task DeleteStoreDeletesStore()
    {
        // Arrange
        const int STORE_TO_DELETE = 1;

        using var client = _app.CreateClient();

        // Act
        var response = await client.DeleteAsync($"/stores/{STORE_TO_DELETE}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task DeleteStoreByNonexistentIdReturns204()
    {
        const int STORE_TO_DELETE = 99999;

        using var client = _app.CreateClient();

        // Act
        var response = await client.DeleteAsync($"/stores/{STORE_TO_DELETE}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task GetStoreByNonNumberIdReturns404()
    {
        const string STORE_TO_DELETE = "penguin";

        using var client = _app.CreateClient();

        // Act
        var response = await client.DeleteAsync($"/stores/{STORE_TO_DELETE}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
