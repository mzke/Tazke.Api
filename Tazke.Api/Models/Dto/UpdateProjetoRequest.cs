namespace Tazke.Api.Models.Dto;

public record UpdateProjetoRequest
{
    public string Titulo { get; set; } = string.Empty;
}
