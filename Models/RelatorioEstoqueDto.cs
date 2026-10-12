namespace JogoApi.Models;

public class RelatorioEstoqueDto
{
    public string Plataforma { get; set; } = null!;
    public int QuantidadeTitulos { get; set; }
    public decimal ValorTotalInventario { get; set; }
}