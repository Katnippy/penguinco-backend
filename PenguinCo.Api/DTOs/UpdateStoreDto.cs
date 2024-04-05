namespace PenguinCo.Api.DTOs;

public record class UpdateStoreDto(
    string Name,
    string Address,
    List<Stock> Stock,
    DateOnly Updated
);
