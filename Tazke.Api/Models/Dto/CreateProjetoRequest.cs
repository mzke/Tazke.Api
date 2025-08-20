namespace Tazke.Api.Models.Dto;

public record CreateProjetoRequest
{
    public long UsuarioId { get; set; }
    public string Titulo { get; set; } = string.Empty;
}
