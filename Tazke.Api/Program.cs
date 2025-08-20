using Microsoft.EntityFrameworkCore;
using Tazke.Api.Data;
using Tazke.Api.Endpoints;
using Tazke.Api.Extensions;
using Microsoft.EntityFrameworkCore.InMemory; // Add this using directive

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsEnvironment("Testing"))
{
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseInMemoryDatabase($"TestDb{Guid.NewGuid()}")
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()
            .LogTo(msg => Console.WriteLine(msg),
                Microsoft.Extensions.Logging.LogLevel.Information
            );
    });
}
else
{
    builder.Services.AddDatabase(builder.Configuration);
}

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddRepositories();
builder.Services.AddBusinessServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();


// Executar migrations automaticamente em desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await context.Database.MigrateAsync();

    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.MapProjetoEndpoints();

app.Run();

public partial class Program { }
