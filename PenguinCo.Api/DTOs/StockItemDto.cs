using System.Text.Json.Serialization;

namespace PenguinCo.Api.DTOs;

public record StockItemDto(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("image")] string Image
);
