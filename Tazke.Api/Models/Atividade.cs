

namespace Tazke.Api.Models;

public class Atividade
{
    public long Id { get; set; }
    public long TarefaId { get; set; }
    public Tarefa Tarefa { get; set; } = null!;
    public DateTime? DataInicial { get; set; }
    public DateTime? DataFinal { get; set; }
    public string? Descricao { get; set; }
}
