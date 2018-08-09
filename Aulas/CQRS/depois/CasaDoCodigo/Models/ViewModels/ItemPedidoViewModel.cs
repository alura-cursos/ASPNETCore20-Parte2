namespace CasaDoCodigo.Models.ViewModels
{
    public class ItemPedidoViewModel
    {
        public int Id { get; set; }
        public string ProdutoCodigo { get; set; }
        public string ProdutoNome { get; set; }
        public decimal PrecoUnitario { get; set; }
        public int Quantidade { get; set; }
        public decimal Subtotal { get; set; }
    }
}
