namespace Tazke.Api.Models;

public class Honorario
{
    public Guid Id { get; set; }
    public Guid UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;
    public string Descricao { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public int Tipo { get; set; }
    public bool Padrao { get; set; }
    public bool Ativo { get; set; }
    public ICollection<Tarefa> Tarefas { get; set; } = [];

}
