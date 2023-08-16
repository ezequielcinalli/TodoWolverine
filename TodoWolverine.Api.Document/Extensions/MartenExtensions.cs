using TodoWolverine.Api.Document.TodoFeatures;
using Weasel.Core;
using Wolverine.Marten;

namespace TodoWolverine.Api.Document.Extensions;

public static class MartenExtensions
{
    public static void AddLocalMarten(this IServiceCollection services, string connectionString, string schema)
    {
        services.AddMarten(opts =>
        {
            opts.Connection(connectionString);
            opts.DatabaseSchemaName = schema;

            opts.UseDefaultSerialization(EnumStorage.AsString);
            opts.Schema.For<Todo>().UniqueIndex(x => x.Description);
        }).UseLightweightSessions().IntegrateWithWolverine();
    }
}