namespace TodoWolverine.Api.Features.Todos;

public record Todo : IDatabaseEntity
{
    public string Description { get; set; } = "";
    public bool IsCompleted { get; set; }
    public Guid Id { get; set; }
}