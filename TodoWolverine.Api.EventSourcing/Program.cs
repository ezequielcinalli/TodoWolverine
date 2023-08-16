using TodoWolverine.Api.EventSourcing.Extensions;
using TodoWolverine.Api.EventSourcing.Middlewares;
using TodoWolverine.Api.EventSourcing.Models;
using TodoWolverine.Api.EventSourcing.TodoFeatures;
using Wolverine;
using Wolverine.FluentValidation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLocalServices(builder.Configuration);
builder.Host.UseWolverine(opts =>
{
    opts.UseFluentValidation();
    opts.Policies.ForMessagesOfType<INewEventStream>().AddMiddleware(typeof(NewEventStreamMiddleware));
    opts.Policies.ForMessagesOfType<IMutable<Todo>>().AddMiddleware(typeof(MutableMiddleware<Todo>));
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();
app.Run();