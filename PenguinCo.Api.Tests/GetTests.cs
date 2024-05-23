using System.Net;
using System.Text.Json;
using PenguinCo.Api.DTOs;

namespace PenguinCo.Api.Tests;

public class GetTests
{
    private readonly TestApp _app = new();

    [Fact]
    public async Task GetAllStoresReadsAllStores()
    {
        // Arrange
        using var client = _app.CreateClient();

        // Act
        HttpResponseMessage response;
        StoreDto storeDtoToCheck;
        using (response = await client.GetAsync("/stores"))
        {
            var content = await response.Content.ReadAsStringAsync();

            List<StoreDto>? storeDtos = null;
            try
            {
                storeDtos = JsonSerializer.Deserialize<List<StoreDto>>(content);
            }
            catch (JsonException)
            {
                Assert.Fail("FAIL: The HTTP response message did not have any content.");
            }
            storeDtoToCheck = storeDtos![1];
        }

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        Assert.True(storeDtoToCheck.Id == 2);
        Assert.True(storeDtoToCheck.Name == "PenguinCo Namibia");
        Assert.True(storeDtoToCheck.Address == "Lüderitz, ǁKaras Region, Namibia");
        Assert.True(storeDtoToCheck.Stock.Count == 4);
        Assert.True(storeDtoToCheck.Updated.Equals(new DateOnly(2024, 4, 25)));
    }

    [Fact]
    public async Task GetStoreByIdReadsMatchingStore()
    {
        // Arrange
        const int STORE_TO_GET = 1;

        using var client = _app.CreateClient();

        // Act
        HttpResponseMessage response;
        StoreDto? storeDto = null;
        using (response = await client.GetAsync($"/stores/{STORE_TO_GET}"))
        {
            var content = await response.Content.ReadAsStringAsync();
            try
            {
                storeDto = JsonSerializer.Deserialize<StoreDto>(content);
            }
            catch (JsonException)
            {
                Assert.Fail("FAIL: The HTTP response message did not have any content.");
            }
        }

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        Assert.True(storeDto!.Id == 1);
        Assert.True(storeDto.Name == "PenguinCo Shrewsbury");
        Assert.True(storeDto.Address == "Shrewsbury, West Midlands, England");
        Assert.True(storeDto.Stock.Count == 2);
        Assert.True(storeDto.Updated.Equals(new DateOnly(2024, 4, 25)));
    }

    [Fact]
    public async Task GetStoreByNonexistentIdReturns404()
    {
        // Arrange
        const int STORE_TO_GET = 99999;

        using var client = _app.CreateClient();

        // Act
        var (response, content) = await TestHelpers.ReturnContentOnReadAsync(client, STORE_TO_GET);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        Assert.Empty(content);
    }

    [Fact]
    public async Task GetStoreByNonNumberIdReturns404()
    {
        // Arrange
        const string STORE_TO_GET = "penguin";

        using var client = _app.CreateClient();

        // Act
        var (response, content) = await TestHelpers.ReturnContentOnReadAsync(client, STORE_TO_GET);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        Assert.Empty(content);
    }
}
