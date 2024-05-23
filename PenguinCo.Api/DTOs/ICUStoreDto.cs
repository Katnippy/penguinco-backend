namespace PenguinCo.Api.DTOs;

public interface ICUStoreDto
{
    string Name { get; init; }
    string Address { get; init; }
    List<Stock> Stock { get; init; }
    DateOnly Updated { get; init; }
}
