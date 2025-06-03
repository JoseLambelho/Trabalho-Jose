// Modelo Favorito
namespace Projeto.Modelo;
public class Favorito
{
    public int Id { get; set}
    public int UtilizadorId { get; set; }
    public Utilizador Utilizador { get; set; }

    public int ReceitaId { get; set; }
    public Receita Receita { get; set; }
}