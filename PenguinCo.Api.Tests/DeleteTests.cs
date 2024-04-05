using Microsoft.AspNetCore.Http.HttpResults;
using PenguinCo.Api.DTOs;
using PenguinCo.Api.Endpoints;

namespace PenguinCo.Api.Tests;

public class DeleteTests
{
    [Fact]
    public void DeleteStoreDeletesStore()
    {
        // Arrange
        var storeIdToDelete = 1;

        // Act
        var result = StoreEndpoints.DeleteStore(storeIdToDelete);

        // Assert
        Assert.IsType<NoContent>(result);
    }
}
