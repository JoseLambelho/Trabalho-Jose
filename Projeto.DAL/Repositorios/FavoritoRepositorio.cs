// Implementação do Repositorio de Receita
using Microsoft.EntityFrameworkCore;
using Projeto.Modelo;
namespace Projeto.Dal.Repositorios
public class FavoritoRepositorio : IFavoritoRepositorio
{
    private readonly ApplicationDbContext _context;

    public FavoritoRepositorio(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Favorito> ObterPorUtilizadorEReceitaAsync(int utilizadorId, int receitaId)
    {
        return await _context.Favoritos
            .FirstOrDefaultAsync(f => f.UtilizadorId == utilizadorId && f.ReceitaId == receitaId);
    }

    public async Task<List<Favorito>> ObterFavoritosPorUtilizadorAsync(int utilizadorId)
    {
        return await _context.Favoritos
            .Include(f => f.Receita)
            .Where(f => f.UtilizadorId == utilizadorId)
            .ToListAsync();
    }

    public async Task AdicionarAsync(Favorito favorito)
    {
        _context.Favoritos.Add(favorito);
        await _context.SaveChangesAsync();
    }

    public async Task RemoverAsync(Favorito favorito)
    {
        _context.Favoritos.Remove(favorito);
        await _context.SaveChangesAsync();
    }
}