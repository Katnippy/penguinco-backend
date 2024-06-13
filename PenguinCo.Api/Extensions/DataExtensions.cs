using Microsoft.EntityFrameworkCore;
using PenguinCo.Api.Data;

namespace PenguinCo.Api.Extensions;

public static class DataExtensions
{
    public static async Task MigrateDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<PenguinCoContext>();
        await dbContext.Database.MigrateAsync();
    }
}
