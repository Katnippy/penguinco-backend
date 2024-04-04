namespace PenguinCo.Api.DTOs;

public class Stock
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; // ? Limit what these can be?
    public int Quantity { get; set; }
}

public record class StoreDto(
    int Id,
    string Name,
    string Address,
    List<Stock> Stock,
    DateOnly Updated
);
