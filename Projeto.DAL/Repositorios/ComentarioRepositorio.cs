// Implementação do Repositorio de Receita
using Microsoft.EntityFrameworkCore;
using Projeto.Modelo;
namespace Projeto.Dal.Repositorios
{

    public class ComentarioRepositorio : IComentarioRepositorio
    {
        private readonly ApplicationDbContext _context;

        public ComentarioRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Comentario>> ObterTodosAsync()
        {
            return await _context.Comentarios.ToListAsync();
        }

        public async Task<Comentario> ObterPorIdAsync(int id)
        {
            return await _context.Comentarios
                .Include(c => c.Utilizador)
                .FirstOrDefaultAsync(c => c.Id == id);
        }


        public async Task AdicionarAsync(Comentario comentario)
        {
            await _context.Comentarios.AddAsync(comentario);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Comentario comentario)
        {
            _context.Comentarios.Update(comentario);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(Comentario comentario)
        {
            _context.Comentarios.Remove(comentario);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Comentario>> ObterPorReceitaAsync(int receitaId)
        {
            return await _context.Comentarios
                .Include(c => c.Utilizador)
                .Where(c => c.ReceitaId == receitaId)
                .ToListAsync();
        }

    }

}