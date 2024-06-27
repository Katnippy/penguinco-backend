using PenguinCo.Api.DTOs;
using PenguinCo.Api.Entities;

namespace PenguinCo.Api.Mapping;

public static class StoreMapping
{
    public static Store ConvertCreateStoreDtoToEntity(this CreateStoreDto newStore)
    {
        return new Store()
        {
            Name = newStore.Name,
            Address = newStore.Address,
            Stock = [],
            Updated = newStore.Updated
        };
    }

    public static StoreDto ConvertEntityToDto(this Store store)
    {
        return new(
            store.StoreId.ToString(),
            store.Name,
            store.Address,
            store
                .Stock.ToList()
                .ConvertAll(stock => new DTOs.Stock
                {
                    Id = stock.StockId.ToString(),
                    StockItemId = stock.StockItemId,
                    Quantity = stock.Quantity
                }),
            store.Updated
        );
    }

    public static ReturnStoreDto ConvertEntityToReturnStoreDto(this Store store)
    {
        return new(
            store.StoreId.ToString(),
            store.Name,
            store.Address,
            store
                .Stock.ToList()
                .ConvertAll(stock => new ReturnStock
                {
                    Id = stock.StockId.ToString(),
                    Name = stock.StockItem!.Name,
                    Quantity = stock.Quantity
                }),
            store.Updated
        );
    }
}
