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

    public async Task<bool> DeleteAsync(Guid id)
    {
        var projeto = await _context.Projetos.FindAsync(id);
        if (projeto == null)
        {
            return false;
        }

        _context.Projetos.Remove(projeto);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Projetos.AnyAsync(p => p.Id == id);
    }

    public Task<IEnumerable<Projeto>> GetAllAsync()
    {
        throw new NotSupportedException("This method is not supported for ProjetoRepository. Use GetByUsuarioAsync instead.");
    }

    public async Task<Projeto?> GetByIdAsync(Guid id)
    {
        var result =  await _context.Projetos.FindAsync(id);
        return result;
    }

    public Task<IEnumerable<Projeto>> GetByUsuarioAsync(Guid usuarioId)
    {
        throw new NotImplementedException();
    }

    public async Task<Projeto> UpdateAsync(Projeto projeto)
    {
        _context.Entry(projeto).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return projeto;
    }
}
