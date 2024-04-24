using System.ComponentModel.DataAnnotations;

namespace PenguinCo.Api.Entities;

public class Store
{
    [Key]
    public int StoreId { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public ICollection<Stock> Stock { get; set; } = [];
    public required DateOnly Updated { get; set; }
}
