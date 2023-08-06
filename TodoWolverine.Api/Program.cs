using TodoWolverine.Api;
using TodoWolverine.Api.Extensions;
using Wolverine;
using Wolverine.FluentValidation;
using Wolverine.Http;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLocalServices(builder.Configuration);
builder.Host.UseWolverine(opts => { opts.UseFluentValidation(); });

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();
app.MapWolverineEndpoints();
app.Run();