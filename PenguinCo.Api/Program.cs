using PenguinCo.Api.Endpoints;

namespace PenguinCo.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.MapStoresEndpoints();

        app.Run();
    }
}
