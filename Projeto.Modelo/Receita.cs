// Modelo Receita
namespace Projeto.Modelo;
public class Receita
{
    public Receita()
        {
            IngredientesReceita = new List<IngredienteReceita>();
            Comentarios = new List<Comentario>();
            Favoritos = new List<Utilizador>();
        }
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public string Categoria { get; set; }
    public string Dificuldade { get; set; }
    public int Duracao { get; set; } // Em minutos
    public string Aprovada { get; set; } = "Evaluating";

    public int UtilizadorId { get; set; }
    public Utilizador Utilizador { get; set; }

    public ICollection<ReceitaIngrediente> ReceitaIngredientes { get; set; } = new List<ReceitaIngrediente>();
    public ICollection<Comentario> Comentarios { get; set; }
    public ICollection<Classificacao> Classificacoes { get; set; } 
        public ICollection<Favorito> Favoritos { get; set; } = new List<Favoritos>();
}