namespace PenguinCo.Api.Entities;

public class Stock
{
    public int StockItemId { get; set; }
    public required StockItem StockItem { get; set; }
    public int Quantity { get; set; }
}

public class Store
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public List<Stock>? Stock { get; set; }
    public DateOnly Updated { get; set; }
}
