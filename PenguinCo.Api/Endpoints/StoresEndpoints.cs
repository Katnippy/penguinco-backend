using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PenguinCo.Api.Common;
using PenguinCo.Api.Data;
using PenguinCo.Api.DTOs;
using PenguinCo.Api.Mapping;

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
            var storeStock = new Entities.Stock
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

    //// PUT
    //public static Results<NoContent, NotFound> PutStore(int id, UpdateStoreDto storeToUpdate)
    //{
    //    var index = _stores.FindIndex(store => store.Id == id);

    //    if (index == -1)
    //    {
    //        return TypedResults.NotFound();
    //    }

    //    _stores[index] = new StoreDto(
    //        id,
    //        storeToUpdate.Name,
    //        storeToUpdate.Address,
    //        storeToUpdate.Stock,
    //        storeToUpdate.Updated
    //    );

    //    return TypedResults.NoContent();
    //}

    //// DELETE
    //public static NoContent DeleteStore(int id)
    //{
    //    _stores.RemoveAll(store => store.Id == id);

    //    return TypedResults.NoContent();
    //}

    public static void MapStoresEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("stores").WithParameterValidation();

        // POST
        // POST /stores
        group.MapPost("/", CreateStoreAsync);

        // GET
        // GET /stores
        group.MapGet("/", ReadAllStoresAsync);

        //GET /stores/1
        group.MapGet("/{id:int}", ReadStoreByIdAsync).WithName(Constants.GET_STORE_ENDPOINT_NAME);

        //// PUT
        //// PUT /stores/1
        //group.MapPut("/{id}", PutStore);

        //// DELETE
        //// DELETE /stores/1
        //group.MapDelete("/{id}", DeleteStore);
    }
}
