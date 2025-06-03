// DbInitializer
using Microsoft.EntityFrameworkCore;
using Projeto.Modelo;
namespace Projeto.DAL;

using Projeto.DAL;
public static class DbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Utilizadores.Any())
            return; // DB já foi semeada

        // Utilizadores
        var utilizadores = new[]
        {
            new Utilizador { Nome = "Admin", Email = "admin@site.com", Password = "123", Tipo = "Administrador" },
            new Utilizador { Nome = "João", Email = "joao@mail.com", Password = "123", Tipo = "Utilizador" }
        };
        context.Utilizadores.AddRange(utilizadores);
        context.SaveChanges();

        // Ingredientes
        var ingredientes = new[]
        {
            new Ingrediente { Nome = "Farinha" },
            new Ingrediente { Nome = "Ovos" },
            new Ingrediente { Nome = "Leite" },
        };
        context.Ingredientes.AddRange(ingredientes);
        context.SaveChanges();

        // Receita
        var receita = new Receita
        {
            Titulo = "Panquecas",
            Descricao = "Misture tudo e frite.",
            Categoria = "Café da manhã",
            Dificuldade = "Fácil",
            Duracao = 15,
            Aprovada = true,
            UtilizadorId = utilizadores[1].Id,
            Ingredientes = new List<ReceitaIngrediente>()
        };
        context.Receitas.Add(receita);
        context.SaveChanges();

        // ReceitaIngredientes
        var receitaIngredientes = new[]
        {
            new ReceitaIngrediente { ReceitaId = receita.Id, IngredienteId = ingredientes[0].Id, Quantidade = 200, Unidade = "g" },
            new ReceitaIngrediente { ReceitaId = receita.Id, IngredienteId = ingredientes[1].Id, Quantidade = 2, Unidade = "un" },
            new ReceitaIngrediente { ReceitaId = receita.Id, IngredienteId = ingredientes[2].Id, Quantidade = 250, Unidade = "ml" },
        };
        context.ReceitaIngredientes.AddRange(receitaIngredientes);
        context.SaveChanges();

        // Comentário
        context.Comentarios.Add(new Comentario
        {
            Texto = "Muito boa!",
            Data = DateTime.Now,
            ReceitaId = receita.Id,
            UtilizadorId = utilizadores[1].Id
        });

        // Classificação
        context.Classificacoes.Add(new Classificacao
        {
            ReceitaId = receita.Id,
            UtilizadorId = utilizadores[1].Id,
            Valor = 5
        });

        // Favorito
        context.Favoritos.Add(new Favorito
        {
            ReceitaId = receita.Id,
            UtilizadorId = utilizadores[1].Id
        });

        context.SaveChanges();
    }
}