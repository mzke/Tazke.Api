using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tazke.Api.Data;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            // Remove qualquer AppDbContext registrado antes
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Usa um banco InMemory único por instância de factory (evita testes contaminando outros)
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase($"TestDb_{Guid.NewGuid()}"));

            // Constrói provider e faz seed inicial
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.Database.EnsureCreated();

            SeedTestData(context);
        });
    }

    public static void SeedTestData(AppDbContext context)
    {
        context.Projetos.AddRange(
            new Tazke.Api.Models.Projeto { Id = Guid.Parse("324d08a8-3595-4d71-8b65-277c76a489e4"), Titulo = "Projeto 1" },
            new Tazke.Api.Models.Projeto { Id = Guid.Parse("3b288c91-956a-42cc-aee1-ebccfa28858b"), Titulo = "Projeto 2" }
        );
        context.SaveChanges();
    }
}
