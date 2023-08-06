using FluentValidation;
using Marten;
using Wolverine.Attributes;

namespace TodoWolverine.Api.Features.Todos;

public record AddTodo(string Description);

public class AddTodoValidator : AbstractValidator<AddTodo>
{
    public const string DescriptionRequired = "Description field is required";
    public const string DescriptionMinimumLength = "Description must be at least 3 characters long";

    public AddTodoValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage(DescriptionRequired)
            .MinimumLength(3).WithMessage(DescriptionMinimumLength);
    }
}

public record TodoCreated(Guid Id, string Description);

public static class AddTodoHandler
{
    [Transactional]
    public static TodoCreated Handle(AddTodo command, IDocumentSession documentSession)
    {
        var todo = new Todo
        {
            Id = Guid.NewGuid(),
            Description = command.Description,
            IsCompleted = false
        };
        documentSession.Events.Append(todo.Id, new TodoCreated(todo.Id, todo.Description));
        var @event = new TodoCreated(todo.Id, todo.Description);
        return @event;
    }
}