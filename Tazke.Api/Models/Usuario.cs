namespace Tazke.Api.Models;

public class Usuario
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public Guid PlanoId { get; set; }
    public Plano Plano { get; set; }
    public ICollection<Honorario> Honorarios { get; set; } = [];
    public ICollection<ProjetoUsuario> ProjetoUsuarios { get; set; } = new List<ProjetoUsuario>();
}
