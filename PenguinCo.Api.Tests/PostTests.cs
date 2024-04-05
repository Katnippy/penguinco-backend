using Microsoft.AspNetCore.Http.HttpResults;
using PenguinCo.Api.DTOs;
using PenguinCo.Api.Endpoints;

namespace PenguinCo.Api.Tests;

public class PostTests
{
    [Fact]
    public void PostStoreCreatesStore()
    {
        // Arrange
        CreateStoreDto newStore =
            new(
                "PenguinCo Madagascar",
                "Antananarivo, Analamanga, Madagascar",
                [
                    new Stock
                    {
                        Id = 1,
                        Name = "Private",
                        Quantity = 98
                    },
                    new Stock
                    {
                        Id = 2,
                        Name = "Skipper",
                        Quantity = 43
                    },
                    new Stock
                    {
                        Id = 1,
                        Name = "Kowalski",
                        Quantity = 48
                    },
                    new Stock
                    {
                        Id = 1,
                        Name = "Rico",
                        Quantity = 42
                    }
                ],
                new DateOnly(2024, 4, 2)
            );

        // Act
        var result = StoresEndpoints.PostStore(newStore);

        // Assert
        Assert.IsType<CreatedAtRoute<StoreDto>>(result);
    }
}
