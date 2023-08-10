using FluentValidation;
using Marten;
using OneOf;
using TodoWolverine.Api.Models;

namespace TodoWolverine.Api.TodoFeatures;

public record AddTodo(string Description) : NewEventStream;

public class AddTodoValidator : AbstractValidator<AddTodo>
{
    public static readonly string DescriptionRequired = "Description field is required";
    public static readonly string DescriptionMinimumLength = "Description must be at least 3 characters long";

    public AddTodoValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage(DescriptionRequired)
            .MinimumLength(3).WithMessage(DescriptionMinimumLength);
    }
}

[GenerateOneOf]
public partial class AddTodoResponse : OneOfBase<ResponseValidationError, Todo>
{
}

public record TodoCreated(Guid Id, string Description) : IDomainEvent;

public static class AddTodoHandler
{
    public static readonly string TodoWithSameDescription = "Todo with same description already exists";

    public static async Task<AddTodoResponse> HandleAsync(AddTodo command, List<IDomainEvent> events,
        IDocumentSession documentSession)
    {
        var todoWithSameDescription = await documentSession.Query<Todo>()
            .FirstOrDefaultAsync(x => x.Description == command.Description);
        if (todoWithSameDescription is not null) return new ResponseValidationError(TodoWithSameDescription);

        var todo = new Todo
        {
            Id = Guid.NewGuid(),
            Description = command.Description,
            IsCompleted = false
        };
        events.Add(new TodoCreated(todo.Id, todo.Description));
        return todo;
    }
}