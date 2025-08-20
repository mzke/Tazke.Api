using Tazke.Api.Models.Dto;
using Tazke.Api.Services;

namespace Tazke.Api.Endpoints;

public static class ProjetoEndpoints
{
    public static void MapProjetoEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/projetos")
            .WithTags("Projetos")
            .WithOpenApi();

        

        group.MapGet("/{id:int}", GetProjetoById)
            .WithName("GetProjetoById")
            .WithSummary("Obter projeto pelo ID");
        group.MapPost("/", CreateProjeto)
            .WithName("CreateProjeto")
            .WithSummary("Cria um novo projeto.")
            .Accepts<CreateProjetoRequest>("application/json")
            .Produces<ProjetoDto>(201)
            .Produces(400);

        group.MapPut("/{id:int}", UpdateProjeto)
            .WithName("UpdateProjeto")
            .WithSummary("Atualiza um projeto.");

        group.MapDelete("/{id:int}", DeleteProjeto)
            .WithName("DeleteProjeto")
            .WithSummary("Exclui um projeto.");
    }
    private static async Task<IResult> GetProjetoById(int id, IProjetoService projetoService)
    {
        var projeto = await projetoService.GetByIdAsync(id);
        return projeto is not null ? Results.Ok(projeto) : Results.NotFound();
    }
    private static async Task<IResult> CreateProjeto(CreateProjetoRequest request, IProjetoService productService)
    {
        try
        {
            var projetoDto = await productService.CreateAsync(request);
            return Results.Created($"/projetos/{projetoDto.Id}", projetoDto);
        }
        catch (ArgumentException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }

    private static async Task<IResult> UpdateProjeto(int id, UpdateProjetoRequest request, 
        IProjetoService projetoService)
    {
        try
        {
            var projetoDto = await projetoService.UpdateAsync(id, request);
            return projetoDto is not null ? Results.Ok(projetoDto) : Results.NotFound();
        }
        catch (ArgumentException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }

    private static async Task<IResult> DeleteProjeto(int id, IProjetoService projetoService)
    {
        var deleted = await projetoService.DeleteAsync(id);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
