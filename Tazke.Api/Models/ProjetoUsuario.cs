namespace Tazke.Api.Models;

public class ProjetoUsuario
{
    public Guid UsuarioId { get; set; }
    public Usuario Usuario { get; set; } 
    public Guid ProjetoId { get; set; }
    public Projeto Projeto { get; set; }
    public Guid PermissaoId { get; set; }
}
