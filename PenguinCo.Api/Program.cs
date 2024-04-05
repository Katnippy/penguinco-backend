using PenguinCo.Api.Common;
using PenguinCo.Api.Endpoints;

namespace PenguinCo.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        // POST
        // POST /stores
        app.MapPost("stores", StoreEndpoints.PostStore);

        // GET
        // GET /stores
        app.MapGet("stores", StoreEndpoints.GetAllStores);

        // GET /stores/1
        app.MapGet("stores/{id}", StoreEndpoints.GetStoreById)
            .WithName(Constants.GET_STORE_ENDPOINT_NAME);

        // PUT
        // PUT /stores/1
        app.MapPut("stores/{id}", StoreEndpoints.PutStore);

        app.Run();
    }
}
