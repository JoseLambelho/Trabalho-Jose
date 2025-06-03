// Modelo Comentario
namespace Projeto.Modelo;
public class Comentario
{
    public int Id { get; set; }
    public string Texto { get; set; }
    public DateTime Data { get; set; }

    public int ReceitaId { get; set; }
    public Receita Receita { get; set; }

    public int UtilizadorId { get; set; }
    public Utilizador Utilizador { get; set; }
}