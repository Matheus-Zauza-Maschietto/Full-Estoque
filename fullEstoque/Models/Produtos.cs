namespace fullEstoque.Models;

public class Produtos 
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Quantidade { get; set; }
    public string Codigo { get; set; }
    public List<string> Tags { get; set; }
    public string Marca { get; set; }
}
