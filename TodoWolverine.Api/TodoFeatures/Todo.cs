using TodoWolverine.Api.Models;

namespace TodoWolverine.Api.TodoFeatures;

public record Todo : IDatabaseEntity
{
    public string Description { get; set; } = "";
    public bool IsCompleted { get; set; }
    public Guid Id { get; set; }
}