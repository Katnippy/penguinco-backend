using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace PenguinCo.Api.Extensions;

public static class ExceptionHandlingExtensions
{
    private static class EventIds
    {
        public static readonly EventId ConcurrencyException = new(0, "ConcurrencyException");
        public static readonly EventId UpdateException = new(1, "UpdateException");
        public static readonly EventId SqlException = new(2, "SQLException");
        public static readonly EventId UnexpectedException = new(3, "UnexpectedException");
    }

    private static async Task LogExceptionAndReturn500(
        ILogger<Program> logger,
        EventId eventId,
        string message,
        Exception exception,
        HttpContext httpContext
    )
    {
        if (eventId.Id != 3)
        {
            logger.LogError(eventId, "{message} {exception}", message, exception);
        }
        else
        {
            logger.LogCritical(eventId, "{message} {exception}", message, exception);
        }

        await TypedResults
            .StatusCode(StatusCodes.Status500InternalServerError)
            .ExecuteAsync(httpContext);
    }

    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.Run(async httpContext =>
        {
            var logger = httpContext.RequestServices.GetRequiredService<ILogger<Program>>();

            var exceptionDetails = httpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = exceptionDetails?.Error;

            switch (exception)
            {
                case DbUpdateConcurrencyException:
                    await LogExceptionAndReturn500(
                        logger,
                        EventIds.ConcurrencyException,
                        "\x1b[1mCouldn't update a concurrently modified entity:\x1b[0m",
                        exception,
                        httpContext
                    );
                    break;

                case DbUpdateException:
                    await LogExceptionAndReturn500(
                        logger,
                        EventIds.UpdateException,
                        "\x1b[1mCouldn't save changes to the database:\x1b[0m",
                        exception,
                        httpContext
                    );
                    break;

                case SqlException:
                    await LogExceptionAndReturn500(
                        logger,
                        EventIds.SqlException,
                        "\x1b[1mA generic SQL exception has occurred:\x1b[0m",
                        exception,
                        httpContext
                    );
                    break;

                default:
                    await LogExceptionAndReturn500(
                        logger,
                        EventIds.UnexpectedException,
                        "\x1b[1mAn unexpected exception has occurred:\x1b[0m",
                        exception!,
                        httpContext
                    );
                    break;
            }
        });
    }
}
