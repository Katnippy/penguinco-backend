using System.ComponentModel.DataAnnotations;

namespace PenguinCo.Api.Entities;

public class Store
{
    [Key]
    public int StoreId { get; set; }

    [MaxLength(50)]
    public required string Name { get; set; }

    [MaxLength(75)]
    public required string Address { get; set; }
    public ICollection<Stock> Stock { get; set; } = [];
    public required DateOnly Updated { get; set; }
}
