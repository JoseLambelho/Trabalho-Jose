// Interface do Repositorio de Receita
using Projeto.Modelo;

public interface IReceitaRepositorio
{
    Task<Utilizador> ObterPorEmailAsync(string email);
        Task<List<Utilizador>> ObterTodosAsync();
        Task<Utilizador> ObterPorIdAsync(int id);
        Task AdicionarAsync(Utilizador utilizador);
        Task AtualizarAsync(Utilizador utilizador);
        Task RemoverAsync(Utilizador utilizador);
        Task BloquearUtilizadorAsync(int id);
        Task DesbloquearUtilizadorAsync(int id);
        Task<List<Utilizador>> ObterUtilizadoresPendentesAsync();

}