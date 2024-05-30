using System.Net;
using PenguinCo.Api.DTOs;

namespace PenguinCo.Api.Tests.StoresTests;

public class GetTests
{
    private readonly TestApp _app = new();

    [Fact]
    public async Task GetAllStoresReadsAllStores()
    {
        // Arrange
        const int STOREDTO_TO_CHECK = 1;

        using var client = _app.CreateClient();

        // Act
        var (response, storeDtos) = await TestHelpers.ReturnDtoOrDtosOnReadAsync<List<StoreDto>>(
            client,
            "/stores"
        );
        var storeDtoToCheck = storeDtos[STOREDTO_TO_CHECK];

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
        var (response, storeDto) = await TestHelpers.ReturnDtoOrDtosOnReadAsync<StoreDto>(
            client,
            $"/stores/{STORE_TO_GET}"
        );

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        Assert.True(storeDto.Id == 1);
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
        var (response, content) = await TestHelpers.ReturnContentOnReadAsync(
            client,
            $"/stores/{STORE_TO_GET}"
        );

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
        var (response, content) = await TestHelpers.ReturnContentOnReadAsync(
            client,
            $"/stores/{STORE_TO_GET}"
        );

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        Assert.Empty(content);
    }
}
