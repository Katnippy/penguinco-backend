using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PenguinCo.Api.Entities;

public class Stock
{
    [Key]
    public int StockId { get; set; }

    [ForeignKey("StockItem")]
    public int StockItemId { get; set; }
    public StockItem? StockItem { get; set; } // ? Why is this nullable?
    public int Quantity { get; set; }

    [ForeignKey("Store")]
    public int StoreId { get; set; }
}
