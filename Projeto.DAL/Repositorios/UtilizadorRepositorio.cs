// Implementação do Repositorio de Utilizador
using Microsoft.EntityFrameworkCore;
using Projeto.Modelo;
namespace Projeto.DAL.Repositorios
{
    public class UtilizadorRepositorio : IUtilizadorRepositorio
    {
        private readonly ApplicationDbContext _context;

        public UtilizadorRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Utilizador> ObterPorEmailAsync(string email)
        {
            return await _context.Utilizadores.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<List<Utilizador>> ObterTodosAsync()
        {
            return await _context.Utilizadores.ToListAsync();
        }

        public async Task<Utilizador> ObterPorIdAsync(int id)
        {
            return await _context.Utilizadores.FindAsync(id);
        }

        public async Task AdicionarAsync(Utilizador utilizador)
        {
            await _context.Utilizadores.AddAsync(utilizador);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Utilizador utilizador)
        {
            _context.Utilizadores.Update(utilizador);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(Utilizador utilizador)
        {
            _context.Utilizadores.Remove(utilizador);
            await _context.SaveChangesAsync();
        }

        public async Task BloquearUtilizadorAsync(int id)
        {
            var utilizador = await _context.Utilizadores.FindAsync(id);
            if (utilizador != null)
            {
                utilizador.Ativo = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DesbloquearUtilizadorAsync(int id)
        {
            var utilizador = await _context.Utilizadores.FindAsync(id);
            if (utilizador != null)
            {
                utilizador.Ativo = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Utilizador>> ObterUtilizadoresPendentesAsync()
        {
            return await _context.Utilizadores
                .Where(u => u.Ativo == false)
                .ToListAsync();
        }
    }
}