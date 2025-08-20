using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Tazke.Api.Data;
using Tazke.Api.Models;
using Xunit;

public class ProjetoEndpointsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly WebApplicationFactory<Program> _factory;

    public ProjetoEndpointsTests(WebApplicationFactory<Program> factory)
    {
        // Substitui a factory padrão pela customizada (com DB isolado e seed)
        _factory = new CustomWebApplicationFactory();

        _client = _factory.CreateClient();
    }

    private static void SeedTestData(AppDbContext context)
    {
        context.Projetos.AddRange(
            new Projeto { Id = Guid.Parse("93fca764-0b25-4734-af94-7178a06c4842"), Titulo = "Projeto 1" },
            new Projeto { Id = Guid.Parse("640abeb8-3cac-47c0-b269-74a8c3073c53"), Titulo = "Projeto 2" }
        );
        var affected = context.SaveChanges();
        Console.WriteLine($"[Seed] Inseridos (SaveChanges): {affected}");
      //  Console.WriteLine($"[Seed] Depois: {context.Projetos.Count()} itens");

    }

    [Fact]
    public async Task GetProjetoById_ReturnsSuccess()
    {
        var response = await _client.GetAsync("/projetos/1");
        response.EnsureSuccessStatusCode();
        
    }

    [Fact]
    public async Task GetProjetoById_ReturnsNotFound_WhenInexistente()
    {
        var response = await _client.GetAsync("/projetos/9999");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task CreateProjeto_ReturnsCreated_WithBody()
    {
        var payload = new { titulo = "Novo Projeto" };
        var response = await _client.PostAsJsonAsync("/projetos/", payload);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var dto = await response.Content.ReadFromJsonAsync<JsonElement>();
        Assert.True(dto.TryGetProperty("id", out _));
        Assert.Equal("Novo Projeto", dto.GetProperty("titulo").GetString());
    }

    [Fact]
    public async Task UpdateProjeto_ReturnsOk_WhenExistente()
    {
        // Arrange: atualiza o projeto 1
        var payload = new { titulo = "Projeto 1 Atualizado" };

        // Act
        var response = await _client.PutAsJsonAsync("/projetos/1", payload);

        // Assert
        response.EnsureSuccessStatusCode();
        var dto = await response.Content.ReadFromJsonAsync<JsonElement>();
        Assert.Equal(1, dto.GetProperty("id").GetInt32());
        Assert.Equal("Projeto 1 Atualizado", dto.GetProperty("titulo").GetString());
    }

    [Fact]
    public async Task UpdateProjeto_ReturnsNotFound_WhenInexistente()
    {
        var payload = new { titulo = "Inexistente" };
        var response = await _client.PutAsJsonAsync("/projetos/9999", payload);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteProjeto_ReturnsNoContent_WhenExistente()
    {
        var response = await _client.DeleteAsync("/projetos/2");
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task DeleteProjeto_ReturnsNotFound_WhenInexistente()
    {
        var response = await _client.DeleteAsync("/projetos/9999");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
