using PenguinCo.Api.Endpoints;

namespace PenguinCo.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        // GET
        // GET /stores
        app.MapGet("stores", StoreEndpoints.GetAllStores);

        // GET /stores/1
        app.MapGet("stores/{id}", StoreEndpoints.GetStoreById);

        app.Run();
    }
}
