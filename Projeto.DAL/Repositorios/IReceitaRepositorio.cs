// Interface do Repositorio de Receita
using Projeto.Modelo;
namespace Projeto.Dal.Repositorios
{
    public interface IReceitaRepositorio
    {
        Task<List<Receita>> ObterTodasAsync();
        Task<Receita> ObterPorIdAsync(int id);
        Task AdicionarAsync(Receita receita);
        Task AtualizarAsync(Receita receita);
        Task RemoverAsync(Receita receita);
        Task<bool> ExcluirAsync(int id);
        Task<List<Receita>> ObterPorEstadoAsync(string estado);
        Task<List<Receita>> ObterPorUtilizadorAsync(int utilizadorId);

        Task<List<Receita>> PesquisarAprovadasAsync(
            string termo = null,
            string categoria = null,
            int dificuldade = null,
            int duracao = null);
    }
}