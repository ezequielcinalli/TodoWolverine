using Microsoft.AspNetCore.Hosting;
using Npgsql;

namespace TodoWolverine.Api.Document.Tests.Fixtures;

public class WebAppFixture : IAsyncLifetime
{
    public IAlbaHost AlbaHost = null!;
    public static string DatabaseName { get; } = "Wolverine_Todo_List_Document_Testing";
    public static string SchemaName { get; } = "testing";

    public static string ConnectionString { get; } =
        $"Host=localhost;Port=5432;Database={DatabaseName};Username=postgres;Password=postgres";

    public static string ConnectionStringWithoutDatabase { get; } =
        "Host=localhost;Port=5432;Username=postgres;Password=postgres";

    public async Task InitializeAsync()
    {
        await CreateDatabaseIfNotExist();
        AlbaHost = await Alba.AlbaHost.For<Program>(builder => { builder.UseEnvironment("Testing"); });
    }

    public async Task DisposeAsync()
    {
        await RemoveSchema(SchemaName);
        try
        {
            await AlbaHost.DisposeAsync();
        }
        catch (Exception ex)
        {
            // ignored
        }
    }

    public async Task CreateDatabaseIfNotExist()
    {
        try
        {
            await using var connection = new NpgsqlConnection(ConnectionStringWithoutDatabase);
            await connection.OpenAsync();

            if (await DatabaseExists(connection)) return;

            await using var command = new NpgsqlCommand(@$"CREATE DATABASE ""{DatabaseName}""", connection);
            await command.ExecuteNonQueryAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error creating database: {ex.Message}", ex);
        }
    }

    private static async Task<bool> DatabaseExists(NpgsqlConnection connection)
    {
        await using var command =
            new NpgsqlCommand($"SELECT 1 FROM pg_database WHERE datname='{DatabaseName}'", connection);
        return await command.ExecuteScalarAsync() is not null;
    }

    public async Task RemoveSchema(string schema)
    {
        try
        {
            await using var connection = new NpgsqlConnection(ConnectionString);
            await connection.OpenAsync();
            await using var command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = $"drop schema {schema} cascade;";
            await command.ExecuteReaderAsync();
        }
        catch (Exception ex)
        {
            // ignored
        }
    }
}