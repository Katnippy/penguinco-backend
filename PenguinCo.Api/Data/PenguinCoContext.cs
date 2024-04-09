using Microsoft.EntityFrameworkCore;
using PenguinCo.Api.Entities;

namespace PenguinCo.Api.Data;

public class PenguinCoContext(DbContextOptions<PenguinCoContext> options) : DbContext(options)
{
    public DbSet<Store> Stores => Set<Store>();
    public DbSet<StockItem> StockItems => Set<StockItem>();
}
