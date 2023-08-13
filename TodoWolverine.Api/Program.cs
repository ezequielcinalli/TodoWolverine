using TodoWolverine.Api.Extensions;
using TodoWolverine.Api.Middlewares;
using TodoWolverine.Api.TodoFeatures;
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