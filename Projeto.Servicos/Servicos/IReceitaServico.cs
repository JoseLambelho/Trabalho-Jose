// Interface do Serviço de Receita
using Projeto.Modelo;

public interface IReceitaServico
{
    Task<List<Receita>> ListarTodasAsync();
    Task<Receita?> ObterPorIdAsync(int id);
    Task<Receita> CriarAsync(Receita receita);
    Task AtualizarAsync(Receita receita);
    Task<bool> RemoverAsync(int id);
}