// Interface do Repositorio de Receita
using Projeto.Modelo;

public interface IReceitaIngredienteRepositorio
{
    Task<ReceitaIngrediente> ObterPorChavesAsync(int receitaId, int ingredienteId);
        Task AdicionarAsync(ReceitaIngrediente receitaIngrediente);
        Task AtualizarAsync(ReceitaIngrediente receitaIngrediente);
        Task RemoverAsync(ReceitaIngrediente receitaIngrediente);
        Task<IEnumerable<ReceitaIngrediente>> ObterTodosAsync(Expression<Func<ReceitaIngrediente, bool>> predicate);
        Task<List<ReceitaIngrediente>> ObterPorIngredientePendenteIdAsync(int ingredientePendenteId);
}