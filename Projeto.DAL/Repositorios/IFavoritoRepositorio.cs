// Interface do Repositorio de Receita
using Projeto.Modelo;
namespace Projeto.DAL.Repositorios
{

    public interface IFavoritoRepositorio
    {
    Task<Favorito> ObterPorUtilizadorEReceitaAsync(int utilizadorId, int receitaId);
    Task<List<Favorito>> ObterFavoritosPorUtilizadorAsync(int utilizadorId);
    Task AdicionarAsync(Favorito favorito);
    Task RemoverAsync(Favorito favorito)
}