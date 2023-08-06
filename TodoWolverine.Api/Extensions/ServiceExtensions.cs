using Marten;
using Marten.Events.Projections;
using Microsoft.AspNetCore.Mvc;
using TodoWolverine.Api.Features.Todos;
using Wolverine.Marten;

namespace TodoWolverine.Api.Extensions;

public static class ServiceExtensions
{
    public static void AddLocalServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add(new ProducesAttribute("application/json"));
            options.Filters.Add(new ProducesResponseTypeAttribute(typeof(ResponseValidationError), 400));
            options.Filters.Add(new ProducesResponseTypeAttribute(typeof(ResponseExceptionError), 500));
        });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddMarten(opts =>
        {
            var connectionString = configuration.GetConnectionString("Marten") ??
                                   throw new Exception("No connection string found for Marten");
            opts.Connection(connectionString);
            opts.DatabaseSchemaName = "todolist";

            opts.Projections.Add<TodoProjection>(ProjectionLifecycle.Inline);

            opts.Schema.For<Todo>().UniqueIndex(x => x.Description);
        }).UseLightweightSessions().IntegrateWithWolverine();
    }
}