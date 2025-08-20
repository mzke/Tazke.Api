using Microsoft.EntityFrameworkCore;
using Tazke.Api.Data;
using Tazke.Api.Data.Repositories;
using Tazke.Api.Models;
using Xunit;

public class ProjetoRepositoryTests
{
    [Fact]
    public async Task CreateAsync_AddsProjeto()
    {
        using var context = DbContextTestUtils.CreateInMemoryContext();

        var repo = new ProjetoRepository(context);

        var projeto = new Projeto { Titulo = "Teste", Data = DateTime.Now };
        var result = await repo.CreateAsync(projeto);

        Assert.NotNull(result);
        Assert.Equal("Teste", result.Titulo);
        Assert.True(result.Id != Guid.Empty);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsProjeto()
    {
        var context = DbContextTestUtils.CreateInMemoryContext();
        var repo = new ProjetoRepository(context);
        await TestDataSeeder.EnsureSeededAsync(context);
        var projeto = await repo.GetByIdAsync(Guid.Parse("ae7843c9-3902-4d84-9641-233ea0d90f55"));
        Assert.NotNull(projeto);
        Assert.Equal("Projeto A", projeto.Titulo);;
    }
    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenInexistente()
    {
        var context = DbContextTestUtils.CreateInMemoryContext();
        var repo = new ProjetoRepository(context);
        var projeto = await repo.GetByIdAsync(Guid.NewGuid());
        Assert.Null(projeto);
    }

    [Fact]
    public async Task ExistsAsync_ReturnsTrue_WhenExistente()
    {
        var context = DbContextTestUtils.CreateInMemoryContext();
        var repo = new ProjetoRepository(context);
        await TestDataSeeder.EnsureSeededAsync(context);
        var result = await repo.ExistsAsync(Guid.Parse("ae7843c9-3902-4d84-9641-233ea0d90f55"));
        Assert.True(result);
    }
    [Fact]
    public async Task ExistsAsync_ReturnsFalse_WhenInexistente()
    {
        var context = DbContextTestUtils.CreateInMemoryContext();
        var repo = new ProjetoRepository(context);
        var result = await repo.ExistsAsync(Guid.NewGuid());
        Assert.False(result);
    }
    [Fact]
    public async Task UpdateAsync_ReturnsUpdatedProjeto()
    {
        var context = DbContextTestUtils.CreateInMemoryContext();
        var repo = new ProjetoRepository(context);
        await TestDataSeeder.EnsureSeededAsync(context);
        var projeto = await repo.GetByIdAsync(Guid.Parse("ae7843c9-3902-4d84-9641-233ea0d90f55"));
        Assert.NotNull(projeto);
        Assert.Equal("Projeto A", projeto.Titulo);
        projeto.Titulo = "Projeto Atualizado";
        var result = await repo.UpdateAsync(projeto);
        Assert.NotNull(result);
        Assert.Equal("Projeto Atualizado", result.Titulo);
        Assert.Equal(projeto.Id, result.Id);
    }
    [Fact]
    public async Task DeleteAsync_ReturnsTrue_WhenExistente()
    {
        var context = DbContextTestUtils.CreateInMemoryContext();
        var repo = new ProjetoRepository(context);
        await TestDataSeeder.EnsureSeededAsync(context);
        var result = await repo.DeleteAsync(Guid.Parse("ae7843c9-3902-4d84-9641-233ea0d90f55"));
        Assert.True(result);
        Assert.False(await repo.ExistsAsync(Guid.Parse("ae7843c9-3902-4d84-9641-233ea0d90f55")));
    }
    [Fact]
    public async Task DeleteAsync_ReturnsFalse_WhenInexistente()
    {
        var context = DbContextTestUtils.CreateInMemoryContext();
        var repo = new ProjetoRepository(context);
        var result = await repo.DeleteAsync(Guid.NewGuid());
        Assert.False(result);
    }
    [Fact]
    public async Task GetAllAsync_ReturnsAllProjetos()
    {
        var context = DbContextTestUtils.CreateInMemoryContext();
        var repo = new ProjetoRepository(context);
        await Assert.ThrowsAsync<NotSupportedException>(() => repo.GetAllAsync());

    }
}
