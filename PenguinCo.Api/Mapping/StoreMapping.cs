using PenguinCo.Api.DTOs;
using PenguinCo.Api.Entities;
using Stock = PenguinCo.Api.Entities.Stock;

namespace PenguinCo.Api.Mapping;

public static class StoreMapping
{
    public static Store ToEntity(this CreateStoreDto newStore)
    {
        return new Store()
        {
            Name = newStore.Name,
            Address = newStore.Address,
            Stock = [],
            Updated = newStore.Updated
        };
    }

    public static StoreDto ToDto(this Store store)
    {
        return new(
            store.StoreId,
            store.Name,
            store.Address,
            store
                .Stock.ToList()
                .ConvertAll(stock => new DTOs.Stock
                {
                    Id = stock.StockId,
                    StockItemId = stock.StockItemId,
                    Quantity = stock.Quantity
                }),
            store.Updated
        );
    }

    public static ReturnStoreDto ToReturnStoreDto(this Store store)
    {
        return new(
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
    }
}
