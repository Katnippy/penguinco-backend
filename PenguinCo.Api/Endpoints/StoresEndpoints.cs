using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PenguinCo.Api.Common;
using PenguinCo.Api.Data;
using PenguinCo.Api.DTOs;
using PenguinCo.Api.Mapping;
using Stock = PenguinCo.Api.Entities.Stock;

namespace PenguinCo.Api.Endpoints;

public static class StoresEndpoints
{
    // POST
    private static async Task<CreatedAtRoute<ReturnStoreDto>> CreateStoreAsync(
        CreateStoreDto newStore,
        PenguinCoContext dbContext
    )
    {
        var store = newStore.ToEntity();

        foreach (var stock in newStore.Stock)
        {
            var storeStock = new Stock
            {
                StockItemId = stock.StockItemId,
                // ? Do we really need to do this every time?
                StockItem = await dbContext.StockItems.FindAsync(stock.StockItemId),
                Quantity = stock.Quantity
            };

            store.Stock.Add(storeStock);
        }

        dbContext.Stores.Add(store);
        await dbContext.SaveChangesAsync(); // ! Exceptions currently aren't handled.

        return TypedResults.CreatedAtRoute(
            store.ToReturnStoreDto(),
            Constants.GET_STORE_ENDPOINT_NAME,
            new { id = store.StoreId }
        );
    }

    // GET
    private static async Task<Ok<List<StoreDto>>> ReadAllStoresAsync(PenguinCoContext dbContext)
    {
        var stores = await dbContext
            .Stores.Include(store => store.Stock)
            .Select(store => store.ToDto())
            .AsNoTracking()
            .ToListAsync();

        return TypedResults.Ok(stores);
    }

    private static async Task<Results<Ok<StoreDto>, NotFound>> ReadStoreByIdAsync(
        int id,
        PenguinCoContext dbContext
    )
    {
        var store = await dbContext
            .Stores.Include(store => store.Stock)
            .AsNoTracking()
            .FirstOrDefaultAsync(store => store.StoreId == id);

        return store != null ? TypedResults.Ok(store.ToDto()) : TypedResults.NotFound();
    }

    // PUT
    private static async Task<Results<NoContent, NotFound>> UpdateStoreAsync(
        int id,
        UpdateStoreDto updatedStore,
        PenguinCoContext dbContext
    )
    {
        var existingStore = await dbContext
            .Stores.Include(store => store.Stock)
            .FirstOrDefaultAsync(store => store.StoreId == id);

        if (existingStore == null)
        {
            return TypedResults.NotFound();
        }
        else
        {
            dbContext.Entry(existingStore).CurrentValues.SetValues(updatedStore);
            dbContext.Stock.RemoveRange(existingStore.Stock);
            foreach (var newStock in updatedStore.Stock)
            {
                await dbContext.Stock.AddAsync(
                    new Stock
                    {
                        StockItemId = newStock.StockItemId,
                        // ? Do we really need to do this every time?
                        StockItem = await dbContext.StockItems.FindAsync(newStock.StockItemId),
                        Quantity = newStock.Quantity,
                        StoreId = id
                    }
                );
            }
            await dbContext.SaveChangesAsync();

            return TypedResults.NoContent();
        }
    }

    // DELETE
    private static async Task<NoContent> DeleteStoreAsync(int id, PenguinCoContext dbContext)
    {
        await dbContext.Stores.Where(store => store.StoreId == id).ExecuteDeleteAsync();

        return TypedResults.NoContent();
    }

    public static void MapStoresEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("stores").WithParameterValidation();

        // POST
        // POST /stores
        group.MapPost("/", CreateStoreAsync);

        // GET
        // GET /stores
        group.MapGet("/", ReadAllStoresAsync);

        // GET /stores/1
        group.MapGet("/{id:int}", ReadStoreByIdAsync).WithName(Constants.GET_STORE_ENDPOINT_NAME);

        // PUT
        // PUT /stores/1
        group.MapPut("/{id:int}", UpdateStoreAsync);

        // DELETE
        // DELETE /stores/1
        group.MapDelete("/{id:int}", DeleteStoreAsync);
    }
}
