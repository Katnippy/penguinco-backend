using System.Net;
using System.Text;
using System.Text.Json;
using PenguinCo.Api.DTOs;
using Stock = PenguinCo.Api.DTOs.Stock;

namespace PenguinCo.Api.Tests;

public class PostTests
{
    private readonly TestApp _app = new();

    [Fact]
    public async Task PostStoreCreatesStore()
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
                new DateOnly(2024, 5, 3)
            );
        var contentToPost = TestHelpers.SerialiseDto(newStore);

        using var client = _app.CreateClient();

        // Act
        HttpResponseMessage response;
        ReturnStoreDto? returnStoreDto = null;
        using (response = await client.PostAsync($"/stores", contentToPost))
        {
            var content = await response.Content.ReadAsStringAsync();
            try
            {
                returnStoreDto = JsonSerializer.Deserialize<ReturnStoreDto>(content);
            }
            catch (JsonException)
            {
                Assert.Fail("FAIL: The HTTP response message did not have any content.");
            }
        }

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        Assert.True(returnStoreDto!.Name == "PenguinCo Madagascar");
        Assert.True(returnStoreDto.Address == "Antananarivo, Analamanga, Madagascar");
        Assert.True(returnStoreDto.Stock.Count == 4);
        Assert.True(returnStoreDto.Updated.Equals(new DateOnly(2024, 5, 3)));
    }

    [Fact]
    public async Task PostStoreWithEmptyStockCreatesStoreWithEmptyStock()
    {
        // Arrange
        CreateStoreDto newStore =
            new(
                "PenguinCo Cape Town",
                "Boulders Beach, Western Cape, South Africa",
                [],
                new DateOnly(2024, 5, 3)
            );
        var contentToPost = TestHelpers.SerialiseDto(newStore);

        using var client = _app.CreateClient();

        // Act
        HttpResponseMessage response;
        ReturnStoreDto? returnStoreDto = null;
        using (response = await client.PostAsync($"/stores", contentToPost))
        {
            var content = await response.Content.ReadAsStringAsync();
            try
            {
                returnStoreDto = JsonSerializer.Deserialize<ReturnStoreDto>(content);
            }
            catch (JsonException)
            {
                Assert.Fail("FAIL: The HTTP response message did not have any content.");
            }
        }

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        Assert.True(returnStoreDto!.Stock != null);
        Assert.True(returnStoreDto.Stock.Count == 0);
    }

    [Fact]
    public async Task PostIncompleteStoreDoesNotCreateStore()
    {
        // Arrange
        StringContent contentToPost =
            new(
                "{\"Name\":\"PenguinCo Cape Town\",\"Address\":\"Boulders Beach, Western Cape, South Africa\",\"Updated\":\"2024-05-03\"}",
                Encoding.UTF8,
                "application/json"
            );

        using var client = _app.CreateClient();

        // Act
        var (response, validationJsonObject) =
            await TestHelpers.ReturnValidationJsonObjectOnCreateAsync(client, contentToPost);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        Assert.True(validationJsonObject!.Errors.Count == 1);
        Assert.Contains("The Stock field is required.", validationJsonObject!.Errors["Stock"]);
    }

    [Fact]
    public async Task PostInvalidStoreDoesNotCreateStore()
    {
        // Arrange
        CreateStoreDto newStore =
            new(
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                "bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb",
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
                new DateOnly(2024, 5, 3)
            );
        var contentToPost = TestHelpers.SerialiseDto(newStore);

        using var client = _app.CreateClient();

        // Act
        var (response, validationJsonObject) =
            await TestHelpers.ReturnValidationJsonObjectOnCreateAsync(client, contentToPost);

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
}
