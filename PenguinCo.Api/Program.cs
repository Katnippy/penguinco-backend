using Microsoft.AspNetCore.HttpLogging;
using PenguinCo.Api.Data;
using PenguinCo.Api.Endpoints;
using PenguinCo.Api.Extensions;

namespace PenguinCo.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connString = builder.Configuration.GetConnectionString("PenguinCo");
        builder.Services.AddSqlServer<PenguinCoContext>(connString);

        builder.Services.AddCors();

        builder.Services.AddHttpLogging(logging =>
        {
            logging.LoggingFields =
                HttpLoggingFields.Duration
                | HttpLoggingFields.RequestBody
                | HttpLoggingFields.RequestHeaders
                | HttpLoggingFields.RequestMethod
                | HttpLoggingFields.RequestPath
                | HttpLoggingFields.ResponseHeaders
                | HttpLoggingFields.ResponseStatusCode;
            logging.RequestBodyLogLimit = 4096;
            logging.CombineLogs = true;
        });

        var app = builder.Build();

        app.MapStoresEndpoints();
        app.MapStockItemsEndpoints();

        app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:5173"));

        app.UseHttpLogging();

        app.UseExceptionHandler(exceptionHandlerApp =>
            exceptionHandlerApp.ConfigureExceptionHandler()
        );

        await app.MigrateDbAsync();

        await app.RunAsync();
    }
}
