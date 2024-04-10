using Microsoft.EntityFrameworkCore;

namespace PenguinCo.Api.Data;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<PenguinCoContext>();
        dbContext.Database.Migrate();
    }
}
