using System.Net;
using System.Text;
using System.Text.Json;
using PenguinCo.Api.DTOs;
using Stock = PenguinCo.Api.DTOs.Stock;

namespace PenguinCo.Api.Tests;

public class PostTests
{
    [Fact]
    public async Task PostStoreCreatesStore()
    {
        // Arrange
        await using TestApp app = new();
        var client = app.CreateClient();

        CreateStoreDto newStore =
            new(
                "PenguinCo Madagascar",
                "Antananarivo, Analamanga, Madagascar",
                [
                    new Stock
                    {
                        Id = 1,
                        StockItemId = 8,
                        Quantity = 98
                    },
                    new Stock
                    {
                        Id = 2,
                        StockItemId = 9,
                        Quantity = 43
                    },
                    new Stock
                    {
                        Id = 1,
                        StockItemId = 10,
                        Quantity = 48
                    },
                    new Stock
                    {
                        Id = 1,
                        StockItemId = 11,
                        Quantity = 42
                    }
                ],
                new DateOnly(2024, 4, 19)
            );
        var jsonString = JsonSerializer.Serialize(newStore);
        StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

        // Act
        var result = await client.PostAsync("/stores", content);

        // Assert
        Assert.Equal(HttpStatusCode.Created, result.StatusCode); // ! Won't work until GET methods are implemented.
        // ? Validate the returned store against our JSON newStore?
    }
}
