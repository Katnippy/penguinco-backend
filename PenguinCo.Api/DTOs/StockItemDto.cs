using System.Text.Json.Serialization;

namespace PenguinCo.Api.DTOs;

public record StockItemDto(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name
);
