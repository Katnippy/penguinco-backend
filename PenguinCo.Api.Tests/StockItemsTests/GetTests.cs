using System.Net;
using PenguinCo.Api.DTOs;

namespace PenguinCo.Api.Tests.StockItemsTests;

public class GetTests
{
    private readonly TestApp _app = new();

    [Fact]
    public async Task GetAllStockItemsReadsAllStockItems()
    {
        // Arrange
        const int ITEM_TO_CHECK = 2;

        using var client = _app.CreateClient();

        // Act
        var (response, stockItemDtos) = await TestHelpers.ReturnDtoOrDtosOnReadAsync<
            List<StockItemDto>
        >(client, "/items");
        var stockItemDtoToCheck = stockItemDtos[ITEM_TO_CHECK];

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        Assert.True(stockItemDtoToCheck.Id == 3);
        Assert.True(stockItemDtoToCheck.Name == "Tux");
    }

    [Fact]
    public async Task GetStoreByIdReadsMatchingStore()
    {
        // Arrange
        const int ITEM_TO_GET = 4;

        using var client = _app.CreateClient();

        // Act
        var (response, stockItemDto) = await TestHelpers.ReturnDtoOrDtosOnReadAsync<StockItemDto>(
            client,
            $"/items/{ITEM_TO_GET}"
        );

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        Assert.True(stockItemDto.Id == 4);
        Assert.True(stockItemDto.Name == "Tuxedosam");
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
            $"/items/{STORE_TO_GET}"
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
            $"/items/{STORE_TO_GET}"
        );

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        Assert.Empty(content);
    }
}
