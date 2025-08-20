using Tazke.Api.Models.Dto;

namespace Tazke.Api.Services;

public interface IProjetoService
{
 
    Task<ProjetoDto?> GetByIdAsync(int id);
    Task<IEnumerable<ProjetoDto>> GetByUsuarioAsync(int categoryId);
    Task<ProjetoDto> CreateAsync(CreateProjetoRequest request);
    Task<ProjetoDto?> UpdateAsync(int id, UpdateProjetoRequest request);
    Task<bool> DeleteAsync(int id);
}
