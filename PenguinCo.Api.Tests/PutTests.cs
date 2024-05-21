using System.Net;
using System.Text;
using PenguinCo.Api.DTOs;

namespace PenguinCo.Api.Tests;

public class PutTests
{
    private readonly TestApp _app = new();

    [Fact]
    public async Task PutStoreUpdatesStore()
    {
        // Arrange
        const int STORE_TO_UPDATE = 2;

        UpdateStoreDto updatedStore =
            new(
                "PenguinCo Namibia",
                "Lüderitz, ǁKaras Region, Namibia",
                [
                    new Stock
                    {
                        Id = 1,
                        StockItemId = 8,
                        Quantity = 14
                    },
                    new Stock
                    {
                        Id = 2,
                        StockItemId = 9,
                        Quantity = 15
                    },
                    new Stock
                    {
                        Id = 3,
                        StockItemId = 10,
                        Quantity = 5
                    },
                    new Stock
                    {
                        Id = 4,
                        StockItemId = 11,
                        Quantity = 20
                    }
                ],
                new DateOnly(2024, 5, 21)
            );
        var contentToPut = TestHelpers.SerialiseUpdateStoreDto(updatedStore);

        using var client = _app.CreateClient();

        // Act
        var (response, content, returnStoreDto) = await TestHelpers.UpdateStoreAndReadUpdatedStore(
            client,
            STORE_TO_UPDATE,
            contentToPut
        );

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        Assert.Empty(content);

        Assert.True(returnStoreDto.Stock[0].Quantity == 14);
    }

    [Fact]
    public async Task PutStoreWithEmptyStockUpdatesStoreWithEmptyStock()
    {
        // Arrange
        const int STORE_TO_UPDATE = 2;

        UpdateStoreDto updatedStore =
            new(
                "PenguinCo Namibia",
                "Lüderitz, ǁKaras Region, Namibia",
                [],
                new DateOnly(2024, 5, 21)
            );
        var contentToPut = TestHelpers.SerialiseUpdateStoreDto(updatedStore);

        using var client = _app.CreateClient();

        // Act
        var (response, content, returnStoreDto) = await TestHelpers.UpdateStoreAndReadUpdatedStore(
            client,
            STORE_TO_UPDATE,
            contentToPut
        );

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        Assert.Empty(content);

        Assert.Empty(returnStoreDto!.Stock);
    }

    [Fact]
    public async Task PutIncompleteStoreDoesNotUpdateStore()
    {
        // Arrange
        const int STORE_TO_UPDATE = 1;

        StringContent contentToPut =
            new(
                "{\"Name\":\"PenguinCo Shrewsbury\",\"Address\":\"Shrewsbury, West Midlands, England\",\"Updated\":\"2024-05-21\"}",
                Encoding.UTF8,
                "application/json"
            );

        using var client = _app.CreateClient();

        // Act
        var (response, validationJsonObject) =
            await TestHelpers.ReturnValidationJsonObjectFromResponse(
                client,
                STORE_TO_UPDATE,
                contentToPut
            );

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        Assert.True(validationJsonObject!.Errors.Count == 1);
        Assert.Contains("The Stock field is required.", validationJsonObject!.Errors["Stock"]);
    }

    [Fact]
    public async Task PutInvalidStoreDoesNotUpdateStore()
    {
        // Arrange
        const int STORE_TO_UPDATE = 2;

        UpdateStoreDto updatedStore =
            new(
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                "bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb",
                [
                    new Stock
                    {
                        Id = 1,
                        StockItemId = 8,
                        Quantity = 15
                    },
                    new Stock
                    {
                        Id = 2,
                        StockItemId = 9,
                        Quantity = 15
                    },
                    new Stock
                    {
                        Id = 3,
                        StockItemId = 10,
                        Quantity = 5
                    },
                    new Stock
                    {
                        Id = 4,
                        StockItemId = 11,
                        Quantity = 20
                    }
                ],
                new DateOnly(2024, 5, 21)
            );
        var contentToPut = TestHelpers.SerialiseUpdateStoreDto(updatedStore);

        using var client = _app.CreateClient();

        // Act
        var (response, validationJsonObject) =
            await TestHelpers.ReturnValidationJsonObjectFromResponse(
                client,
                STORE_TO_UPDATE,
                contentToPut
            );

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        Assert.True(validationJsonObject!.Errors.Count == 2);
        Assert.Contains(
            "The field Name must be a string with a maximum length of 50.",
            validationJsonObject!.Errors["Name"]
        );
        Assert.Contains(
            "The field Address must be a string with a maximum length of 75.",
            validationJsonObject!.Errors["Address"]
        );
    }

    [Fact]
    public async Task PutStoreInOrderUpdatesStoreInOrder()
    {
        // Arrange
        var STORE_TO_UPDATE = 2;

        UpdateStoreDto firstStoreUpdate =
            new(
                "PenguinCo Namibia",
                "Lüderitz, ǁKaras Region, Namibia",
                [
                    new Stock
                    {
                        Id = 1,
                        StockItemId = 8,
                        Quantity = 15
                    },
                    new Stock
                    {
                        Id = 2,
                        StockItemId = 9,
                        Quantity = 15
                    },
                    new Stock
                    {
                        Id = 3,
                        StockItemId = 10,
                        Quantity = 5
                    },
                    new Stock
                    {
                        Id = 4,
                        StockItemId = 11,
                        Quantity = 19
                    }
                ],
                new DateOnly(2024, 5, 21)
            );
        var firstContentToPut = TestHelpers.SerialiseUpdateStoreDto(firstStoreUpdate);

        UpdateStoreDto secondStoreUpdate =
            new(
                "PenguinCo Namibia",
                "Lüderitz, ǁKaras Region, Namibia",
                [
                    new Stock
                    {
                        Id = 1,
                        StockItemId = 8,
                        Quantity = 14
                    },
                    new Stock
                    {
                        Id = 2,
                        StockItemId = 9,
                        Quantity = 15
                    },
                    new Stock
                    {
                        Id = 3,
                        StockItemId = 10,
                        Quantity = 5
                    },
                    new Stock
                    {
                        Id = 4,
                        StockItemId = 11,
                        Quantity = 18
                    }
                ],
                new DateOnly(2024, 5, 21)
            );
        var secondContentToPut = TestHelpers.SerialiseUpdateStoreDto(secondStoreUpdate);

        using var client = _app.CreateClient();

        // Act
        var (firstResponse, firstContent, _) = await TestHelpers.UpdateStoreAndReadUpdatedStore(
            client,
            STORE_TO_UPDATE,
            firstContentToPut
        );
        var (secondResponse, secondContent, returnStoreDto) =
            await TestHelpers.UpdateStoreAndReadUpdatedStore(
                client,
                STORE_TO_UPDATE,
                secondContentToPut
            );

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, firstResponse.StatusCode);
        Assert.Equal(HttpStatusCode.NoContent, secondResponse.StatusCode);

        Assert.Empty(firstContent);
        Assert.Empty(secondContent);

        Assert.True(returnStoreDto!.Stock[3].Quantity == 18);
    }
}
