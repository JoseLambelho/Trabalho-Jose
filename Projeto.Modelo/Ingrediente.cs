// Modelo Ingrediente
namespace Projeto.Modelo;
public class Ingrediente
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public ICollection<ReceitaIngrediente> ReceitaIngredientes { get; set; }
}