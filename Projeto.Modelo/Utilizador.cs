// Modelo Utilizador
namespace Projeto.Modelo;
public class Utilizador
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    // Tipo pode ser "Utilizador" ou "Administrador"
    public string Tipo { get; set; }

    public ICollection<Receita> Receitas { get; set; }
    public ICollection<Comentario> Comentarios { get; set; }
    public ICollection<Classificacao> Classificacoes { get; set; }
    public ICollection<Favorito> Favoritos { get; set; }
}