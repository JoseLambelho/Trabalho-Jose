using ReceitasPlus.API.DTOs;
using ReceitasPlus.DAL.Repositories;
using ReceitasPlus.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceitasPlus.API.Services
{
    public class ComentarioService : IComentarioService
    {
        private readonly IComentarioRepository _repo;

        public ComentarioService(IComentarioRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<ComentarioReadDto>> ObterTodosComentariosAsync()
        {
            var comentarios = await _repo.ObterTodosAsync();
            return comentarios.Select(c => new ComentarioReadDto
            {
                Id = c.Id,
                Texto = c.Texto,
                ReceitaId = c.ReceitaId,
                UtilizadorId = c.UtilizadorId,
                UtilizadorNome = c.Utilizador?.Nome ?? "Anónimo",
                Data = c.Data
            }).ToList();
        }

        public async Task<ComentarioReadDto> ObterComentarioPorIdAsync(int id)
        {
            var c = await _repo.ObterPorIdAsync(id);
            if (c == null) return null;
            return new ComentarioReadDto
            {
                Id = c.Id,
                Texto = c.Texto,
                ReceitaId = c.ReceitaId,
                UtilizadorId = c.UtilizadorId,
                UtilizadorNome = c.Utilizador?.Nome ?? "Anónimo",
                Data = c.Data
            };
        }

        public async Task<ComentarioReadDto> CriarComentarioAsync(ComentarioCreateDto dto, int utilizadorId)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "O DTO do comentário não pode ser nulo.");

            if (string.IsNullOrWhiteSpace(dto.Texto))
                throw new ArgumentException("O texto do comentário é obrigatório.");

            if (dto.ReceitaId <= 0)
                throw new ArgumentException("ReceitaId inválido.");

            if (utilizadorId <= 0) 
                throw new ArgumentException("UtilizadorId inválido.");

            var comentario = new Comentario
            {
                Texto = dto.Texto,
                ReceitaId = dto.ReceitaId,
                UtilizadorId = utilizadorId, 
                Data = DateTime.Now
            };

            await _repo.AdicionarAsync(comentario);

            var comentarioCompleto = await _repo.ObterPorIdAsync(comentario.Id);

            return new ComentarioReadDto
            {
                Id = comentarioCompleto.Id,
                Texto = comentarioCompleto.Texto,
                ReceitaId = comentarioCompleto.ReceitaId,
                UtilizadorId = comentarioCompleto.UtilizadorId,
                UtilizadorNome = comentarioCompleto.Utilizador?.Nome ?? "Anónimo",
                Data = comentarioCompleto.Data
            };
        }


        public async Task<bool> AtualizarComentarioAsync(int id, ComentarioUpdateDto dto)
        {
            var comentario = await _repo.ObterPorIdAsync(id);
            if (comentario == null) return false;

            comentario.Texto = dto.Texto;
            comentario.ReceitaId = dto.ReceitaId;
            comentario.UtilizadorId = dto.UtilizadorId;

            await _repo.AtualizarAsync(comentario);
            return true;
        }

        public async Task<bool> RemoverComentarioAsync(int id)
        {
            var comentario = await _repo.ObterPorIdAsync(id);
            if (comentario == null) return false;

            await _repo.RemoverAsync(comentario);
            return true;
        }

        public async Task<List<ComentarioReadDto>> ObterComentariosPorReceitaAsync(int receitaId)
        {
            var comentarios = await _repo.ObterPorReceitaAsync(receitaId);
            return comentarios.Select(c => new ComentarioReadDto
            {
                Id = c.Id,
                Texto = c.Texto,
                ReceitaId = c.ReceitaId,
                UtilizadorId = c.UtilizadorId,
                UtilizadorNome = c.Utilizador?.Nome ?? "Anónimo",
                Data = c.Data
            }).ToList();
        }
    }
}