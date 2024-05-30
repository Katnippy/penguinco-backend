using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PenguinCo.Api.Common;
using PenguinCo.Api.Data;
using PenguinCo.Api.DTOs;
using PenguinCo.Api.Mapping;

namespace PenguinCo.Api.Endpoints;

public static class StockItemsEndpoints
{
    // GET
    private static async Task<Ok<List<StockItemDto>>> ReadAllStockItemsAsync(
        PenguinCoContext dbContext
    )
    {
        var items = await dbContext
            .StockItems.Select(item => item.ConvertEntityToDto())
            .AsNoTracking()
            .ToListAsync();

        return TypedResults.Ok(items);
    }

    private static async Task<Results<Ok<StockItemDto>, NotFound>> ReadStockItemByIdAsync(
        int id,
        PenguinCoContext dbContext
    )
    {
        var item = await dbContext
            .StockItems.AsNoTracking()
            .FirstOrDefaultAsync(item => item.StockItemId == id);

        return item != null ? TypedResults.Ok(item.ConvertEntityToDto()) : TypedResults.NotFound();
    }

    public static void MapStockItemsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("items").WithParameterValidation();

        // GET
        // GET /stores
        group.MapGet("/", ReadAllStockItemsAsync);

        // GET /stores/1
        group
            .MapGet("/{id:int}", ReadStockItemByIdAsync)
            .WithName(Constants.GET_ITEM_ENDPOINT_NAME);
    }
}
