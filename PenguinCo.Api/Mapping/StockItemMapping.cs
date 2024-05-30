using PenguinCo.Api.DTOs;
using PenguinCo.Api.Entities;

namespace PenguinCo.Api.Mapping;

public static class StockItemMapping
{
    public static StockItemDto ConvertEntityToDto(this StockItem stockItem) =>
        new(stockItem.StockItemId, stockItem.Name);
}
