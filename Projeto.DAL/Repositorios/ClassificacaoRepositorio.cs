using Microsoft.EntityFrameworkCore;
using Projeto.Modelo;

namespace Projeto.DAL.Repositorios
{
    public interface IReceitaRepositorio
    {
        Task AddOrUpdateRatingAsync(int receitaId, int userId, int classificacao);
        Task<(double Media, int Total)> GetAverageAndTotalRatingsAsync(int receitaId);
    }

    public class ClassificacaoRepositorio : IClassificacaoRepositorio
    {
        private readonly ReceitasPlusContext _context;

        public ClassificacaoRepositorio(ReceitasPlusContext context)
        {
            _context = context;
        }

        public async Task AddOrUpdateRatingAsync(int receitaId, int userId, int rating)
        {
            var existingRating = await _context.Classificacoes
                .FirstOrDefaultAsync(r => r.ReceitaId == receitaId && r.UtilizadorId == userId);

            if (existingRating != null)
            {
                existingRating.Rating = rating;
                existingRating.Data = DateTime.UtcNow;
                _context.RecipeRatings.Update(existingRating);
            }
            else
            {
                var newRating = new Classificacao
                {
                    ReceitaId = receitaId,
                    UtilizadorId = userId,
                    Rating = rating,
                    Data = DateTime.UtcNow
                };
                _context.Classificacoes.Add(newRating);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<(double Media, int Total)> GetAverageAndTotalRatingsAsync(int receitaId)
        {
            var ratings = await _context.Classificacoes
                .Where(r => r.ReceitaId == receitaId)
                .Select(r => r.Rating)
                .ToListAsync();

            if (ratings.Count == 0)
                return (0, 0);

            return (ratings.Average(), ratings.Count);
        }
    }
}