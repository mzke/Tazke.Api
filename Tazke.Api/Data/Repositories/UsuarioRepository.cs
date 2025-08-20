using Microsoft.EntityFrameworkCore;
using Tazke.Api.Models;

namespace Tazke.Api.Data.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public Task<IEnumerable<Usuario>> GetAllAsync()
    {
        throw new NotSupportedException("Não suportado.");
    }

    public async Task<Usuario?> GetByIdAsync(Guid id)
    {
        var result = await _context.Usuarios.FindAsync(id);
        return result;
    }

    public async Task<Usuario> CreateAsync(Usuario model)
    {
        _context.Usuarios.Add(model);
        _context.SaveChanges();
        return model;
    }

    public async Task<Usuario> UpdateAsync(Usuario model)
    {
        _context.Usuarios.Update(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null)
        {
            return false;
        }

        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Usuarios.AnyAsync(u => u.Id == id);;
    }
}