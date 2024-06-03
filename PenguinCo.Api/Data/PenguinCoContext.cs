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
                    Updated = new DateOnly(2024, 4, 25),
                },
                new Store
                {
                    StoreId = 2,
                    Name = "PenguinCo Namibia",
                    Address = "Lüderitz, ǁKaras Region, Namibia",
                    Updated = new DateOnly(2024, 4, 25),
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
                },
                new Stock
                {
                    StockId = 3,
                    StockItemId = 8,
                    Quantity = 15,
                    StoreId = 2
                },
                new Stock
                {
                    StockId = 4,
                    StockItemId = 9,
                    Quantity = 15,
                    StoreId = 2
                },
                new Stock
                {
                    StockId = 5,
                    StockItemId = 10,
                    Quantity = 5,
                    StoreId = 2
                },
                new Stock
                {
                    StockId = 6,
                    StockItemId = 11,
                    Quantity = 20,
                    StoreId = 2
                }
            );

        modelBuilder
            .Entity<StockItem>()
            .HasData(
                new StockItem
                {
                    StockItemId = 1,
                    Name = "Pingu",
                    Image = "../../images/pingu.jpg"
                },
                new StockItem
                {
                    StockItemId = 2,
                    Name = "Pinga",
                    Image = "../../images/pinga.jpg"
                },
                new StockItem
                {
                    StockItemId = 3,
                    Name = "Tux",
                    Image = "../../images/tux.jpg"
                },
                new StockItem
                {
                    StockItemId = 4,
                    Name = "Tuxedosam",
                    Image = "../../images/tuxedosam.jpg"
                },
                new StockItem
                {
                    StockItemId = 5,
                    Name = "Suica",
                    Image = "../../images/suica.jpg"
                },
                new StockItem
                {
                    StockItemId = 6,
                    Name = "Donpen",
                    Image = "../../images/donpen.jpg"
                },
                new StockItem
                {
                    StockItemId = 7,
                    Name = "Pen Pen",
                    Image = "../../images/penpen.jpg"
                },
                new StockItem
                {
                    StockItemId = 8,
                    Name = "Private",
                    Image = "../../images/private.jpg"
                },
                new StockItem
                {
                    StockItemId = 9,
                    Name = "Skipper",
                    Image = "../../images/skipper.jpg"
                },
                new StockItem
                {
                    StockItemId = 10,
                    Name = "Kowalski",
                    Image = "../../images/kowalski.jpg"
                },
                new StockItem
                {
                    StockItemId = 11,
                    Name = "Rico",
                    Image = "../../images/rico.jpg"
                }
            );
    }
}
