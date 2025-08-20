namespace Tazke.Api.Models;

public class Tarefa
{
    public Guid Id { get; set; }
    public Guid ProjetoId { get; set; }
    public Projeto Projeto { get; set; } = null!;
    public Guid HonorarioId { get; set; }
    public Honorario Honorario { get; set; } = null!;
    public DateTime Data { get; set; }
    public string Assunto { get; set; } = string.Empty;
    public long Tempo { get; set; }
    public decimal PrecoTotal { get; set; }
    public bool Concluido { get; set; }
    public int Numero { get; set; }
    public ICollection<Atividade> Atividades { get; set; } = [];
}
