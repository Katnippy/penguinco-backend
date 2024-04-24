using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using PenguinCo.Api.Data;

namespace PenguinCo.Api.Tests;

public class TestApp : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(
            (context, services) =>
            {
                services.RemoveAll(typeof(DbContextOptions<PenguinCoContext>));
                services.AddDbContext<PenguinCoContext>(options =>
                {
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("PenguinCoTest")
                    );
                });
            }
        );

        return base.CreateHost(builder);
    }
}
