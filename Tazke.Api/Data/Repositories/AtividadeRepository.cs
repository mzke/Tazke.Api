using Microsoft.EntityFrameworkCore;
using Tazke.Api.Models;

namespace Tazke.Api.Data.Repositories;

public class AtividadeRepository :  IAtividadeRepository
{
    private readonly AppDbContext _context;

    public AtividadeRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Atividade> CreateAsync(Atividade atividade)
    {
        _context.Atividades.Add(atividade);
        await _context.SaveChangesAsync();
        return atividade;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var atividade = await _context.Atividades.FindAsync(id);
        if (atividade == null)
        {
            return false;
        }

        _context.Atividades.Remove(atividade);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Atividades.AnyAsync(p => p.Id == id);
    }
    public async Task<IEnumerable<Atividade>> GetByTarefaAsync(Guid tarefaId)
    {
        return await _context.Atividades
            .Include(p => p.Tarefa)
            .Where(p => p.TarefaId == tarefaId)
            .OrderBy(p => p.Descricao)
            .ToListAsync();
    }
    public Task<IEnumerable<Atividade>> GetAllAsync()
    {
        throw new NotSupportedException("This method is not supported for AtividadeRepository. Use GetByTarefaAsync instead.");
    }

    public async Task<Atividade?> GetByIdAsync(Guid id)
    {
        return await _context.Atividades.FirstOrDefaultAsync(p => p.Id == id );
    }

    public async Task<Atividade> UpdateAsync(Atividade atividade)
    {
        _context.Entry(atividade).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return atividade;
    }
}
