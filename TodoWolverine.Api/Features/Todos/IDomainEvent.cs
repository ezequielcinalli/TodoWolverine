namespace TodoWolverine.Api.Features.Todos;

public interface IDomainEvent
{
    public Guid Id { get; init; }
}