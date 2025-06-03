// Interface do Repositorio de Receita
using Projeto.Modelo;

public interface IIngredienteRepositorio
{
    Task<List<Ingrediente>> ObterTodosAsync();
        Task<Ingrediente> ObterPorIdAsync(int id);
        Task AdicionarAsync(Ingrediente ingrediente);
        Task AtualizarAsync(Ingrediente ingrediente);
        Task RemoverAsync(Ingrediente ingrediente);
        Task<List<Ingrediente>> ObterPorEstadoAsync(string estado);
        Task<bool> ExisteIngredienteAsync(string nome, string unidade, string estado)
}