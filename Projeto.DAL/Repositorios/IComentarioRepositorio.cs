// Interface do Repositorio de Comentario
using Projeto.Modelo;
namespace Projeto.DAL.Repositorios
{

    public interface IComentarioRepositorio
    {
        Task<List<Comentario>> ObterTodosAsync();
        Task<Comentario> ObterPorIdAsync(int id);
        Task AdicionarAsync(Comentario comentario);
        Task AtualizarAsync(Comentario comentario);
        Task RemoverAsync(Comentario comentario);
        Task<List<Comentario>> ObterPorReceitaAsync(int receitaId);
    }
}