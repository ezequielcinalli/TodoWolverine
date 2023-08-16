namespace TodoWolverine.Api.Document.TodoFeatures;

public record AddTodo(string Description) : INewDocument<Todo>;

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

public record TodoCreated(Guid Id, string Description) : IDomainEvent;

public static class AddTodoHandler
{
    public static readonly string TodoWithSameDescription = "Todo with same description already exists";

    public static async Task<Result<Todo>> HandleAsync(AddTodo command, List<Todo> documents,
        IDocumentSession documentSession)
    {
        var todoWithSameDescription = await documentSession.Query<Todo>()
            .FirstOrDefaultAsync(x => x.Description == command.Description);
        if (todoWithSameDescription is not null) return Result.Fail(TodoWithSameDescription);

        var todo = new Todo
        {
            Id = Guid.NewGuid(),
            Description = command.Description,
            IsCompleted = false
        };
        documents.Add(todo);
        return todo;
    }
}