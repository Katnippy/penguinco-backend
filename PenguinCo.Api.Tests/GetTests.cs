using Microsoft.AspNetCore.Http.HttpResults;
using PenguinCo.Api.DTOs;
using PenguinCo.Api.Endpoints;

namespace PenguinCo.Api.Tests;

public class GetTests
{
    [Fact]
    public void GetAllStoresReturnsAllStores()
    {
        // Arrange

        // Act
        var result = StoreEndpoints.GetAllStores();

        // Assert
        Assert.IsType<Ok<List<StoreDto>>>(result);
    }

    [Fact]
    public void GetStoreByIdReturnsMatchingStore()
    {
        // Arrange
        var storeToGet = 2;

        // Act
        var result = StoreEndpoints.GetStoreById(storeToGet);

        // Assert
        Assert.IsType<Ok<StoreDto>>(result);
    }
}
