using Microsoft.EntityFrameworkCore;
using Tazke.Api.Models;

namespace Tazke.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Atividade> Atividades { get; set; }
    public DbSet<Honorario> Honorarios { get; set; }
    public DbSet<Plano> Planos { get; set; }
    public DbSet<Projeto> Projetos { get; set; }
    public DbSet<ProjetoUsuario> ProjetoUsuarios { get; set; }
    public DbSet<Tarefa> Tarefas { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Atividade
        modelBuilder.Entity<Atividade>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Tarefa)
                  .WithMany(o => o.Atividades)
                  .HasForeignKey(e => e.TarefaId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
        // Honorario
        modelBuilder.Entity<Honorario>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Usuario)
                  .WithMany(o => o.Honorarios)
                  .HasForeignKey(e => e.UsuarioId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
        // Plano
        modelBuilder.Entity<Plano>(entity =>
        {
            entity.HasKey(e => e.Id);
        });
        // Projeto
        modelBuilder.Entity<Projeto>(entity =>
        {
            entity.HasKey(e => e.Id);
        });
        // ProjetoUsuario
        modelBuilder.Entity<ProjetoUsuario>(entity =>
        {
            entity.HasKey(e => new { e.ProjetoId, e.UsuarioId });
            entity.HasOne(pu => pu.Usuario)
                  .WithMany(o => o.ProjetoUsuarios)
                  .HasForeignKey(pu => pu.UsuarioId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(pu => pu.Projeto)
                  .WithMany(o => o.ProjetoUsuarios)
                  .HasForeignKey(pu => pu.ProjetoId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
        // Tarefa
        modelBuilder.Entity<Tarefa>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.HasOne(t => t.Honorario)
                  .WithMany(o => o.Tarefas)
                  .HasForeignKey(t => t.HonorarioId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(t => t.Projeto)
                  .WithMany(o => o.Tarefas)
                  .HasForeignKey(t => t.ProjetoId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
        // Usuario
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.HasOne(u=> u.Plano)
                  .WithMany(o=> o.Usuarios)
                  .HasForeignKey(u=> u.PlanoId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
        SeedData(modelBuilder);
     }
    private static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Plano>().HasData(
            new Plano { Id = 1,  MaxAtividades = 2, MaxProjetos = 2, MaxTarefas = 2, Nome = "Gratuito" }
        );
    }
}
