using Microsoft.EntityFrameworkCore;
using Tazke.Api.Data;
using Tazke.Api.Models;

public static class TestDataSeeder
{
    public static async Task EnsureSeededAsync(AppDbContext context)
    {
        // Idempotência: não semear se já houver registros básicos
        if (await context.Projetos.AnyAsync()) return;

        context.Planos.Add(
            new Plano
            {
                Id = Guid.Parse("12345678-1234-1234-1234-123456789012"), Nome = "Plano A", MaxAtividades = 10,
                MaxProjetos = 10, MaxTarefas = 10
            }
        );
        context.Usuarios.AddRange(
            new Usuario
            {
                Id = Guid.Parse("84bfc8b5-5f27-4f6c-889c-99f25c03dd5c"),
                PlanoId = Guid.Parse("12345678-1234-1234-1234-123456789012"), Email = "usuario1@tazke.app"
            }
        );
        context.Projetos.AddRange(
            new Projeto
            {
                Id = Guid.Parse("ae7843c9-3902-4d84-9641-233ea0d90f55"), Titulo = "Projeto A",
                Data = DateTime.UtcNow.AddDays(-2)
            },
            new Projeto
            {
                Id = Guid.Parse("99cb79d8-fb88-4143-9224-7311de1d3a8c"), Titulo = "Projeto B",
                Data = DateTime.UtcNow.AddDays(-1)
            }
        );
        context.ProjetoUsuarios.AddRange(
            new ProjetoUsuario
            {
                ProjetoId = Guid.Parse("ae7843c9-3902-4d84-9641-233ea0d90f55"),
                UsuarioId = Guid.Parse("84bfc8b5-5f27-4f6c-889c-99f25c03dd5c")
            },
            new ProjetoUsuario
            {
                ProjetoId = Guid.Parse("99cb79d8-fb88-4143-9224-7311de1d3a8c"),
                UsuarioId = Guid.Parse("84bfc8b5-5f27-4f6c-889c-99f25c03dd5c")
            }
        );
        await context.SaveChangesAsync();
    }

    public static async Task SeedProjetosAsync(AppDbContext ctx, params Projeto[] projetos)
    {
        ctx.Projetos.AddRange(projetos);
        await ctx.SaveChangesAsync();
    }
}