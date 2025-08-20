namespace Tazke.Api.Models;

public class Plano
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int MaxProjetos { get; set; }
    public int MaxTarefas { get; set; }
    public int MaxAtividades { get; set; }
    public ICollection<Usuario> Usuarios { get; set; } = [];
}
