// Implementação do Repositorio de Receita
using Microsoft.EntityFrameworkCore;
using Projeto.Modelo;
namespace Projeto.DAL.Repositorios
{
    public class ReceitaRepositorio : IReceitaRepositorio
    {
        private readonly ApplicationDbContext _context;

        public ReceitaRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

public async Task<List<Receita>> ObterTodasAsync()
        {
            return await _context.Receitas
                .Include(r => r.Utilizador)
                .Include(r => r.ReceitaIngredientes)
                .Include(r => r.Comentarios)
                .Include(r => r.Favoritos)
                .ToListAsync();
        }

        public async Task<Receita> ObterPorIdAsync(int id)
        {
            return await _context.Receitas
                .Include(r => r.Utilizador)
                .Include(r => r.ReceitaIngredientes)
                 .ThenInclude(ir => ir.Ingrediente)
                .Include(r => r.Comentarios)
                .Include(r => r.Favoritos)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AdicionarAsync(Receita receita)
        {
            await _context.Receitas.AddAsync(receita);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Receita receita)
        {
            _context.Receitas.Update(receita);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(Receita receita)
        {
            _context.Receitas.Remove(receita);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExcluirAsync(int id)
        {
            var receita = await _context.Receitas
                .Include(r => r.Comentarios)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (receita == null) return false;

            _context.Receitas.Remove(receita);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Receita>> ObterPorAprovadaAsync(string aprovada)
        {
            return await _context.Receitas
                .Include(r => r.Utilizador)
                .Include(r => r.Classificacoess)
                .Include(r => r.ReceitaIngredientes)      
            .ThenInclude(ir => ir.Ingrediente)
                .Where(r => r.Aprovada == aprovada)
                .ToListAsync();
        }

        public async Task<List<Receita>> ObterPorUtilizadorAsync(int utilizadorId)
        {
            return await _context.Receitas
                .Include(r => r.Utilizador)
                .Where(r => r.UtilizadorId == utilizadorId)
                .ToListAsync();
        }
        public async Task<List<Receita>> PesquisarAprovadasAsync(
        string termo = null,
        string categoria = null,
        string dificuldade = null,
        int? duracaoMax = null)
        {
            var query = _context.Receitas
                .Include(r => r.Utilizador)
                .Include(r => r.Classificacoes)
                .Include(r => r.ReceitaIngredientes)
                    .ThenInclude(ir => ir.Ingrediente)
                .Where(r => r.Aprovada == "Aprovada");

          
            if (!string.IsNullOrWhiteSpace(termo))
            {
                termo = termo.Trim().ToLower();
                query = query.Where(r =>
                    r.Nome.ToLower().Contains(termo) ||
                    r.Descricao.ToLower().Contains(termo) ||
                    r.Categoria.ToLower().Contains(termo) ||
                    r.  ReceitaIngredientes.Any(ir =>
                        ir.Ingrediente.Nome.ToLower().Contains(termo)
                    ));
            }

            if (!string.IsNullOrWhiteSpace(categoria))
                query = query.Where(r => r.Categoria == categoria);

            if (!string.IsNullOrWhiteSpace(dificuldade))
                query = query.Where(r => r.Dificuldade == dificuldade);

            if (duracaoMax.HasValue)
                query = query.Where(r => r.Duracao <= duracaoMax.Value);

            return await query.ToListAsync();
        }
    }
}
