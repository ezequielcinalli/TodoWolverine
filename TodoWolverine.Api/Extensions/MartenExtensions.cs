using Marten;
using Marten.Events.Projections;
using TodoWolverine.Api.TodoFeatures;
using Wolverine.Marten;

namespace TodoWolverine.Api.Extensions;

public static class MartenExtensions
{
    public static void AddLocalMarten(this IServiceCollection services, string connectionString, string schema)
    {
        services.AddMarten(opts =>
        {
            opts.Connection(connectionString);
            opts.DatabaseSchemaName = schema;

            opts.Projections.Add<TodoProjection>(ProjectionLifecycle.Inline);

            opts.Schema.For<Todo>().UniqueIndex(x => x.Description);
        }).UseLightweightSessions().IntegrateWithWolverine();
    }
}