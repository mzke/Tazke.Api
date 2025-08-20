namespace Tazke.Api.Models;

public class Projeto
{
    public long Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public DateTime Data { get; set; }
    public long Tempo { get; set; }
    public decimal? PrecoTotal { get; set; }
    public bool Arquivado { get; set; }
    public string Sigla { get; set; } = string.Empty;
    public int NumeroTarefa { get; set; }
    public ICollection<Tarefa> Tarefas { get; set; } = new List<Tarefa>();
    public ICollection<ProjetoUsuario> ProjetoUsuarios { get; set; } = new List<ProjetoUsuario>();

}
