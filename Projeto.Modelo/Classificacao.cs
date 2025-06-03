// Modelo Classificacao
using Projeto.Modelo;
public class Classificacao
{
    public int Id { get; set; }
    public int Valor { get; set; } // Ex: de 1 a 5 estrelas

    public int ReceitaId { get; set; }
    public Receita Receita { get; set; }

    public int UtilizadorId { get; set; }
    public Utilizador Utilizador { get; set; }
}