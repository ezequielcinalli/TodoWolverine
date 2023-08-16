using Microsoft.AspNetCore.Mvc;

namespace TodoWolverine.Api.Document.Extensions;

public static class ServiceExtensions
{
    public static void AddLocalServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add(new ProducesResponseTypeAttribute(typeof(ResponseValidationError), 400));
            options.Filters.Add(new ProducesResponseTypeAttribute(typeof(ResponseExceptionError), 500));
        });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        var martenConnection = configuration.GetConnectionString("MartenConnection") ??
                               throw new Exception("No connection string found for MartenConnection");
        var schema = configuration.GetConnectionString("MartenSchema") ??
                     throw new Exception("No connection string found for MartenSchema");
        services.AddLocalMarten(martenConnection, schema);
    }
}