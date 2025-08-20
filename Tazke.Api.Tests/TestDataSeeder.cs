using Microsoft.EntityFrameworkCore;
using Tazke.Api.Data;
using Tazke.Api.Models;

public static class TestDataSeeder
{
    public static async Task EnsureSeededAsync(AppDbContext ctx)
    {
        // Idempotência: não semear se já houver registros básicos
        if (await ctx.Projetos.AnyAsync()) return;

        ctx.Projetos.AddRange(
            new Projeto { Id = Guid.Parse("ae7843c9-3902-4d84-9641-233ea0d90f55") ,Titulo = "Projeto A", Data = DateTime.UtcNow.AddDays(-2) },
            new Projeto { Id = Guid.Parse("99cb79d8-fb88-4143-9224-7311de1d3a8c"), Titulo = "Projeto B", Data = DateTime.UtcNow.AddDays(-1) }
        );

        await ctx.SaveChangesAsync();
    }

    public static async Task SeedProjetosAsync(AppDbContext ctx, params Projeto[] projetos)
    {
        ctx.Projetos.AddRange(projetos);
        await ctx.SaveChangesAsync();
    }
}
