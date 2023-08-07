namespace TodoWolverine.Api.Models;

public interface IDomainEvent
{
    public Guid Id { get; init; }
}