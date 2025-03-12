using TicTacToe.Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
  .AddApplicationPart(typeof(PresentationDependencyInjection).Assembly).Services
  .AddPresentation()
  .AddInfrastructure()
  .AddApplication();

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.MapControllers();

app.Run();
