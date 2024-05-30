using PenguinCo.Api.Data;
using PenguinCo.Api.Endpoints;

namespace PenguinCo.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connString = builder.Configuration.GetConnectionString("PenguinCo");
        builder.Services.AddSqlServer<PenguinCoContext>(connString);

        var app = builder.Build();

        app.MapStoresEndpoints();
        app.MapStockItemsEndpoints();

        await app.MigrateDbAsync();

        await app.RunAsync();
    }
}
