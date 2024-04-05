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
        app.MapPost("stores", StoresEndpoints.PostStore);

        // GET
        // GET /stores
        app.MapGet("stores", StoresEndpoints.GetAllStores);

        // GET /stores/1
        app.MapGet("stores/{id}", StoresEndpoints.GetStoreById)
            .WithName(Constants.GET_STORE_ENDPOINT_NAME);

        // PUT
        // PUT /stores/1
        app.MapPut("stores/{id}", StoresEndpoints.PutStore);

        // DELETE
        // DELETE /stores/1
        app.MapDelete("stores/{id}", StoresEndpoints.DeleteStore);

        app.Run();
    }
}
