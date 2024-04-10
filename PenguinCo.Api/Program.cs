using PenguinCo.Api.Data;
using PenguinCo.Api.Endpoints;

namespace PenguinCo.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connString = builder.Configuration.GetConnectionString("PenguinCo");
        builder.Services.AddSqlServer<PenguinCoContext>(connString);

        var app = builder.Build();

        app.MapStoresEndpoints();

        app.MigrateDb();

        app.Run();
    }
}
