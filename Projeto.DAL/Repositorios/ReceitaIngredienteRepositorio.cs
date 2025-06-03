// Implementação do Repositorio de ReceitaIngrediente
using Microsoft.EntityFrameworkCore;
using Projeto.Modelo;
namespace Projeto.Dal.Repositorios
{

    public class ReceitaIngredienteRepositorio : IReceitaRepositorio
    {
        private readonly ApplicationDbContext _context;
public async Task<IEnumerable<ReceitaIngrediente>> ObterTodosAsync(Expression<Func<ReceitaIngrediente, bool>> predicate)
        {
            return await _context.ReceitaIngredientes
                .Where(predicate)
                .ToListAsync();
        }

      
        public async Task<ReceitaIngrediente> ObterPorChavesAsync(int receitaId, int ingredienteId)
        {
            return await _context.ReceitaIngredientes 
                .FirstOrDefaultAsync(ir => ir.ReceitaId == receitaId && ir.IngredienteId == ingredienteId);
        }

        public async Task AdicionarAsync(ReceitaIngrediente ReceitaIngrediente)
        {
            await _context.ReceitaIngredientes.AddAsync(ReceitaIngrediente); 
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(ReceitaIngrediente ReceitaIngrediente)
        {
            _context.ReceitaIngredientes.Update(ReceitaIngrediente); 
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(ReceitaIngrediente ReceitaIngrediente)
        {
            _context.ReceitaIngredientes.Remove(ReceitaIngrediente); 
            await _context.SaveChangesAsync();
        }

        public async Task<List<ReceitaIngrediente>> ObterPorIngredientePendenteIdAsync(int ingredientePendenteId)
        {
            return await _context.ReceitaIngredientes
                .Where(ir => ir.IngredientePendenteId == ingredientePendenteId)
                .ToListAsync();
        }
    }
}