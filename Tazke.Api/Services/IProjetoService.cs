using Tazke.Api.Models.Dto;

namespace Tazke.Api.Services;

public interface IProjetoService
{
 
    Task<ProjetoDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<ProjetoDto>> GetByUsuarioAsync(Guid usuarioId);
    Task<ProjetoDto> CreateAsync(CreateProjetoRequest request);
    Task<ProjetoDto?> UpdateAsync(Guid id, UpdateProjetoRequest request);
    Task<bool> DeleteAsync(Guid id);
}
