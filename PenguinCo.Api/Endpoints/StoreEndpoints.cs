using Microsoft.AspNetCore.Http.HttpResults;
using PenguinCo.Api.Common;
using PenguinCo.Api.DTOs;

namespace PenguinCo.Api.Endpoints;

public static class StoreEndpoints
{
    public static List<StoreDto> stores =
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
                stores[^1].Id + 1,
                newStore.Name,
                newStore.Address,
                newStore.Stock,
                newStore.Updated
            );

        stores.Add(store);

        return TypedResults.CreatedAtRoute(
            store,
            Constants.GET_STORE_ENDPOINT_NAME,
            new { id = store.Id }
        );
    }

    // GET
    public static Ok<List<StoreDto>> GetAllStores() => TypedResults.Ok(stores);

    public static Ok<StoreDto> GetStoreById(int id) =>
        TypedResults.Ok(stores.Find(store => store.Id == id));
}
