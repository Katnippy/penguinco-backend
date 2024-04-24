namespace PenguinCo.Api.DTOs;

public class Stock
{
    public int Id { get; set; }
    public int StockItemId { get; set; }
    public int Quantity { get; set; }
}

public record class StoreDto(
    int Id,
    string Name,
    string Address,
    List<Stock> Stock,
    DateOnly Updated
);
