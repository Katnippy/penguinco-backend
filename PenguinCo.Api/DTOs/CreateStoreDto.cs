namespace PenguinCo.Api.DTOs;

public record class CreateStoreDto(
    string Name,
    string Address,
    List<Stock> Stock,
    DateOnly Updated
);
