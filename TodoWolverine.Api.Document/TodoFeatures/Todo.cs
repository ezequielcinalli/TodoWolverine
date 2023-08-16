namespace TodoWolverine.Api.Document.TodoFeatures;

public record Todo : IGuid
{
    public string Description { get; set; } = "";
    public bool IsCompleted { get; set; }
    public Guid Id { get; init; }
}