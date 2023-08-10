using TodoWolverine.Api;
using TodoWolverine.Api.Extensions;
using TodoWolverine.Api.Models;
using TodoWolverine.Api.TodoFeatures;
using Wolverine;
using Wolverine.FluentValidation;
using Wolverine.Http;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLocalServices(builder.Configuration);
builder.Host.UseWolverine(opts =>
{
    opts.UseFluentValidation();
    opts.Policies.ForMessagesOfType<NewEventStream>().AddMiddleware(typeof(NewEventStreamMiddleware));
    opts.Policies.ForMessagesOfType<IMutableTodo>().AddMiddleware(typeof(MutableTodoMiddleware));
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();
app.MapWolverineEndpoints();
app.Run();