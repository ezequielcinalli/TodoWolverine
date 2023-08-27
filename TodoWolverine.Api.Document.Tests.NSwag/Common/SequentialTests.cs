namespace TodoWolverine.Api.Document.Tests.NSwag.Common;

public static class SequentialTests
{
    public const string Definition = "SequentialTests";
}

[CollectionDefinition(SequentialTests.Definition)]
public class SequentialTestsCollection : ICollectionFixture<SequentialTestsCollectionFixture>
{
}

public class SequentialTestsCollectionFixture : IDisposable
{
    public void Dispose()
    {
    }
}