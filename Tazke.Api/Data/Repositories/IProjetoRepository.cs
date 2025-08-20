using Tazke.Api.Models;

namespace Tazke.Api.Data.Repositories;

public interface IProjetoRepository : IRepository<Projeto>
{
    Task<IEnumerable<Projeto>> GetByUsuarioAsync(Guid usuarioId);
}
