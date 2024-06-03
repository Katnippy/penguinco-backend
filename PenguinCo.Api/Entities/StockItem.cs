using System.ComponentModel.DataAnnotations;

namespace PenguinCo.Api.Entities;

public class StockItem
{
    [Key]
    public int StockItemId { get; set; }
    public required string Name { get; set; } // ? Limit length?
    public required string Image { get; set; } // ? Limit length?
}
