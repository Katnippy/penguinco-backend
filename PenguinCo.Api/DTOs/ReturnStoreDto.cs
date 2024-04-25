namespace PenguinCo.Api.DTOs;

public class ReturnStock
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
}

public record class ReturnStoreDto(
    int Id,
    string Name,
    string Address,
    List<ReturnStock> Stock,
    DateOnly Updated
);
