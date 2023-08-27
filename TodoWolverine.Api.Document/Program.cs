using TodoWolverine.Api.Document.Middlewares;
using TodoWolverine.Api.Document.TodoFeatures;
using Wolverine.FluentValidation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLocalServices(builder.Configuration);
builder.Host.UseWolverine(opts =>
{
    opts.UseFluentValidation();
    opts.Policies.ForMessagesOfType<INewDocument<Todo>>().AddMiddleware(typeof(NewDocumentMiddleware<Todo>));
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

public partial class Program
{
}