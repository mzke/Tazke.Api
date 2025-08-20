namespace Tazke.Api.Models;

public class ProjetoUsuario
{
    public long UsuarioId { get; set; }
    public Usuario Usuario { get; set; } 
    public long ProjetoId { get; set; }
    public Projeto Projeto { get; set; }
    public long PermissaoId { get; set; }
}
