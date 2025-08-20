using Microsoft.EntityFrameworkCore;
using Tazke.Api.Models;

namespace Tazke.Api.Data.Repositories;

public class ProjetoRepository :  IProjetoRepository
{
    private readonly AppDbContext _context;

    public ProjetoRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Projeto> CreateAsync(Projeto projeto)
    {
        _context.Projetos.Add(projeto);
        await _context.SaveChangesAsync();
        return projeto;
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Projeto>> GetAllAsync()
    {
        throw new NotSupportedException("This method is not supported for ProjetoRepository. Use GetByUsuarioAsync instead.");
    }

    public Task<Projeto?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Projeto>> GetByUsuarioAsync(int usuarioId)
    {
        throw new NotImplementedException();
    }

    public Task<Projeto> UpdateAsync(Projeto product)
    {
        throw new NotImplementedException();
    }
}
