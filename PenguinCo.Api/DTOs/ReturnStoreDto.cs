using System.Text.Json.Serialization;

namespace PenguinCo.Api.DTOs;

public class ReturnStock
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
}

public record ReturnStoreDto(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("address")] string Address,
    [property: JsonPropertyName("stock")] List<ReturnStock> Stock,
    [property: JsonPropertyName("updated")] DateOnly Updated
);
