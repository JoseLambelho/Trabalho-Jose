// Implementação do Repositorio de Ingrediente
using Microsoft.EntityFrameworkCore;
using Projeto.Modelo;
namespace Projeto.Dal.Repositorios
{
public class IngredienteRepositorio : IIngredienteRepositorio
{
    public async Task<List<Ingrediente>> ObterTodosAsync()
        {
            return await _context.Ingredientes.ToListAsync();
        }

        public async Task<Ingrediente> ObterPorIdAsync(int id)
        {
            return await _context.Ingredientes.FindAsync(id);
        }

        public async Task AdicionarAsync(Ingrediente ingrediente)
        {
            await _context.Ingredientes.AddAsync(ingrediente);
            await _context.SaveChangesAsync();

        }

        public async Task AtualizarAsync(Ingrediente ingrediente)
        {
            _context.Ingredientes.Update(ingrediente);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(Ingrediente ingrediente)
        {
            _context.Ingredientes.Remove(ingrediente);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Ingrediente>> ObterPorEstadoAsync(string estado)
        {
            return await _context.Ingredientes
                .Where(i => i.Estado == estado)
                .ToListAsync();
        }

        public async Task<bool> ExisteIngredienteAsync(string nome, string unidade, string estado)
        {
            return await _context.Set<Ingrediente>()
                .AnyAsync(i => i.Nome == nome && i.Unidade == unidade && i.Estado == estado);
        }
    }
}