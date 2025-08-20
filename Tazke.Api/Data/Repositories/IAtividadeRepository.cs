using Tazke.Api.Models;

namespace Tazke.Api.Data.Repositories;
public interface IAtividadeRepository : IRepository<Atividade>
{
    Task<IEnumerable<Atividade>> GetByTarefaAsync(Guid tarefaId);
}
