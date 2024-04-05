using Microsoft.AspNetCore.Http.HttpResults;
using PenguinCo.Api.DTOs;
using PenguinCo.Api.Endpoints;

namespace PenguinCo.Api.Tests;

public class PutTests
{
    [Fact]
    public void PutStoreUpdatesStore()
    {
        // Arrange
        var storeIdToUpdate = 3;
        UpdateStoreDto storeToUpdate =
            new(
                "PenguinCo Islington",
                "Islington, London, England",
                [
                    new Stock
                    {
                        Id = 1,
                        Name = "Pingu",
                        Quantity = 23
                    },
                    new Stock
                    {
                        Id = 2,
                        Name = "Pinga",
                        Quantity = 8
                    },
                    new Stock
                    {
                        Id = 3,
                        Name = "Tuxedosam",
                        Quantity = 0
                    }
                ],
                new DateOnly(2024, 4, 5)
            );

        // Act
        var result = StoreEndpoints.PutStore(storeIdToUpdate, storeToUpdate);

        // Assert
        Assert.IsType<NoContent>(result);
    }
}
