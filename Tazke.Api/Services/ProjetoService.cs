using Tazke.Api.Data.Repositories;
using Tazke.Api.Models;
using Tazke.Api.Models.Dto;

namespace Tazke.Api.Services;

public class ProjetoService : IProjetoService
{
    private readonly IProjetoRepository _projetoRepository;
    public ProjetoService(IProjetoRepository projetoRepository)
    {
        _projetoRepository = projetoRepository;
    }
    public async Task<ProjetoDto?> GetByIdAsync(int id)
    {
        var projeto = await _projetoRepository.GetByIdAsync(id);
        if (projeto == null)
        {
            return null;
        }
        var result = new ProjetoDto
        {
            Id = projeto.Id,
            Titulo = projeto.Titulo
        };
        return result;
    }
    public async Task<IEnumerable<ProjetoDto>> GetByUsuarioAsync(int usuarioId)
    {
        var projetos = await _projetoRepository.GetByUsuarioAsync(usuarioId);
        return projetos.Select(p => new ProjetoDto
        {
            Id = p.Id,
            Titulo = p.Titulo
        });
    }
    public async Task<ProjetoDto> CreateAsync(CreateProjetoRequest request)
    {
        var projeto = new Projeto
        {
            Titulo = request.Titulo, 
            
        };
        var result =  await _projetoRepository.CreateAsync(projeto);
        return new ProjetoDto
        {
            Id = result.Id,
            Titulo = result.Titulo
        };
    }
    public async Task<ProjetoDto?> UpdateAsync(int id, UpdateProjetoRequest request)
    {
        var projeto = await _projetoRepository.GetByIdAsync(id);
        if (projeto == null)
        {
            return null;
        }
        projeto.Titulo = request.Titulo;
        var result =  await _projetoRepository.UpdateAsync(projeto);
        return new ProjetoDto
        {
            Id = result.Id,
            Titulo = result.Titulo
        };
    }
    public async Task<bool> DeleteAsync(int id)
    {
        return await _projetoRepository.DeleteAsync(id);
    }
}
