using Microsoft.EntityFrameworkCore;
using PenguinCo.Api.Entities;

namespace PenguinCo.Api.Data;

public class PenguinCoContext(DbContextOptions<PenguinCoContext> options) : DbContext(options)
{
    public DbSet<Store> Stores => Set<Store>();
    public DbSet<Stock> Stock => Set<Stock>();
    public DbSet<StockItem> StockItems => Set<StockItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Store>()
            .HasData(
                new Store
                {
                    StoreId = 1,
                    Name = "PenguinCo Shrewsbury",
                    Address = "Shrewsbury, West Midlands, England",
                    Updated = new DateOnly(2024, 4, 12),
                }
            );

        modelBuilder
            .Entity<Stock>()
            .HasData(
                new Stock
                {
                    StockId = 1,
                    StockItemId = 1,
                    Quantity = 10,
                    StoreId = 1
                },
                new Stock
                {
                    StockId = 2,
                    StockItemId = 2,
                    Quantity = 5,
                    StoreId = 1,
                }
            );

        modelBuilder
            .Entity<StockItem>()
            .HasData(
                new StockItem { StockItemId = 1, Name = "Pingu" },
                new StockItem { StockItemId = 2, Name = "Pinga" },
                new StockItem { StockItemId = 3, Name = "Tux" },
                new StockItem { StockItemId = 4, Name = "Tuxedosam" },
                new StockItem { StockItemId = 5, Name = "Suica" },
                new StockItem { StockItemId = 6, Name = "Donpen" },
                new StockItem { StockItemId = 7, Name = "Pen Pen" },
                new StockItem { StockItemId = 8, Name = "Private" },
                new StockItem { StockItemId = 9, Name = "Skipper" },
                new StockItem { StockItemId = 10, Name = "Kowalski" },
                new StockItem { StockItemId = 11, Name = "Rico" }
            );
    }
}
