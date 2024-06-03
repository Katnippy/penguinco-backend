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

        builder.Services.AddCors();

        var app = builder.Build();

        app.MapStoresEndpoints();
        app.MapStockItemsEndpoints();

        app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:5173"));

        await app.MigrateDbAsync();

        await app.RunAsync();
    }
}
