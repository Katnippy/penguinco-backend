using Microsoft.AspNetCore.Http.HttpResults;
using PenguinCo.Api.Common;
using PenguinCo.Api.Data;
using PenguinCo.Api.DTOs;
using PenguinCo.Api.Entities;

namespace PenguinCo.Api.Endpoints;

public static class StoresEndpoints
{
    // POST
    public static async Task<CreatedAtRoute<ReturnStoreDto>> CreateStoreAsync(
        CreateStoreDto newStore,
        PenguinCoContext dbContext
    )
    {
        Store store =
            new()
            {
                Name = newStore.Name,
                Address = newStore.Address,
                Stock = [],
                Updated = newStore.Updated
            };

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

        ReturnStoreDto storeDto =
            new(
                store.StoreId,
                store.Name,
                store.Address,
                store
                    .Stock.ToList()
                    .ConvertAll(stock => new ReturnStock
                    {
                        Id = stock.StockId,
                        Name = stock.StockItem!.Name,
                        Quantity = stock.Quantity
                    }),
                store.Updated
            );

        return TypedResults.CreatedAtRoute(
            storeDto,
            Constants.GET_STORE_ENDPOINT_NAME,
            new { id = store.StoreId }
        );
    }

    //// GET
    //public static Ok<List<StoreDto>> GetAllStores() => TypedResults.Ok(_stores);

    //public static Results<Ok<StoreDto>, NotFound> GetStoreById(int id)
    //{
    //    StoreDto? store = _stores.Find(store => store.Id == id);

    //    return store != null ? TypedResults.Ok(store) : TypedResults.NotFound();
    //}

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

    public static RouteGroupBuilder MapStoresEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("stores").WithParameterValidation();

        // POST
        // POST /stores
        group.MapPost("/", CreateStoreAsync);

        //// GET
        //// GET /stores
        //group.MapGet("/", GetAllStores);

        // GET /stores/1
        //group.MapGet("/{id}", GetStoreById).WithName(Constants.GET_STORE_ENDPOINT_NAME);

        //// PUT
        //// PUT /stores/1
        //group.MapPut("/{id}", PutStore);

        //// DELETE
        //// DELETE /stores/1
        //group.MapDelete("/{id}", DeleteStore);

        return group;
    }
}
