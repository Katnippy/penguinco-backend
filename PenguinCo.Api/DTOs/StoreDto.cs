using System.Text.Json.Serialization;

namespace PenguinCo.Api.DTOs;

public class Stock
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("stockItemId")]
    public int StockItemId { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
}

public record StoreDto(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("address")] string Address,
    [property: JsonPropertyName("stock")] List<Stock> Stock,
    [property: JsonPropertyName("updated")] DateOnly Updated
);
