using Microsoft.AspNetCore.Http.HttpResults;
using PenguinCo.Api.Common;
using PenguinCo.Api.DTOs;

namespace PenguinCo.Api.Endpoints;

public static class StoresEndpoints
{
    private static readonly List<StoreDto> _stores =
    [
        new(
            1,
            "PenguinCo Shrewsbury",
            "Shrewsbury, West Midlands, England",
            [
                new Stock
                {
                    Id = 1,
                    Name = "Pingu",
                    Quantity = 10
                },
                new Stock
                {
                    Id = 2,
                    Name = "Pinga",
                    Quantity = 5
                }
            ],
            new DateOnly(2024, 4, 2)
        ),
        new(
            2,
            "PenguinCo Birmingham Superstore",
            "Birmingham, West Midlands, England",
            [
                new Stock
                {
                    Id = 1,
                    Name = "Pingu",
                    Quantity = 47
                },
                new Stock
                {
                    Id = 2,
                    Name = "Pinga",
                    Quantity = 33
                },
                new Stock
                {
                    Id = 3,
                    Name = "Tux",
                    Quantity = 3
                },
                new Stock
                {
                    Id = 4,
                    Name = "Tuxedosam",
                    Quantity = 14
                },
                new Stock
                {
                    Id = 5,
                    Name = "Suica",
                    Quantity = 10
                },
                new Stock
                {
                    Id = 6,
                    Name = "Donpen",
                    Quantity = 8
                },
            ],
            new DateOnly(2024, 4, 2)
        ),
        new(
            3,
            "PenguinCo Islington",
            "Islington, London, England",
            [
                new Stock
                {
                    Id = 1,
                    Name = "Pingu",
                    Quantity = 23
                },
                new Stock
                {
                    Id = 2,
                    Name = "Pinga",
                    Quantity = 8
                },
                new Stock
                {
                    Id = 3,
                    Name = "Tuxedosam",
                    Quantity = 1
                }
            ],
            new DateOnly(2024, 4, 2)
        )
    ];

    // POST
    public static CreatedAtRoute<StoreDto> PostStore(CreateStoreDto newStore)
    {
        StoreDto store =
            new(
                _stores[^1].Id + 1,
                newStore.Name,
                newStore.Address,
                newStore.Stock,
                newStore.Updated
            );

        _stores.Add(store);

        return TypedResults.CreatedAtRoute(
            store,
            Constants.GET_STORE_ENDPOINT_NAME,
            new { id = store.Id }
        );
    }

    // GET
    public static Ok<List<StoreDto>> GetAllStores() => TypedResults.Ok(_stores);

    public static Results<Ok<StoreDto>, NotFound> GetStoreById(int id)
    {
        StoreDto? store = _stores.Find(store => store.Id == id);

        return store != null ? TypedResults.Ok(store) : TypedResults.NotFound();
    }

    // PUT
    public static Results<NoContent, NotFound> PutStore(int id, UpdateStoreDto storeToUpdate)
    {
        var index = _stores.FindIndex(store => store.Id == id);

        if (index == -1)
        {
            return TypedResults.NotFound();
        }

        _stores[index] = new StoreDto(
            id,
            storeToUpdate.Name,
            storeToUpdate.Address,
            storeToUpdate.Stock,
            storeToUpdate.Updated
        );

        return TypedResults.NoContent();
    }

    // DELETE
    public static NoContent DeleteStore(int id)
    {
        _stores.RemoveAll(store => store.Id == id);

        return TypedResults.NoContent();
    }

    public static RouteGroupBuilder MapStoresEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("stores");

        // POST
        // POST /stores
        group.MapPost("/", PostStore);

        // GET
        // GET /stores
        group.MapGet("/", GetAllStores);

        // GET /stores/1
        group.MapGet("/{id}", GetStoreById).WithName(Constants.GET_STORE_ENDPOINT_NAME);

        // PUT
        // PUT /stores/1
        group.MapPut("/{id}", PutStore);

        // DELETE
        // DELETE /stores/1
        group.MapDelete("/{id}", DeleteStore);

        return group;
    }
}
