using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Controllers added but DbContext NOT registered (bad)
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
