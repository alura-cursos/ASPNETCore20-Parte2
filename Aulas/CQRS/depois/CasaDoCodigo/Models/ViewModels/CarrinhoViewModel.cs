using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Models.ViewModels
{
    public class CarrinhoViewModel
    {
        public CarrinhoViewModel(IList<ItemPedidoViewModel> itens)
        {
            Itens = itens;
        }

        public CarrinhoViewModel(IList<ItemPedido> itens)
        {
            Itens = itens.Select(i =>
            new ItemPedidoViewModel
                {
                    Id = i.Id,
                    ProdutoCodigo = i.Produto.Codigo,
                    ProdutoNome = i.Produto.Nome,
                    PrecoUnitario = i.PrecoUnitario,
                    Quantidade = i.Quantidade,
                    Subtotal = i.Subtotal
                }
            ).ToList();
        }

        public IList<ItemPedidoViewModel> Itens { get; }

        public decimal Total => Itens.Sum(i => i.Quantidade * i.PrecoUnitario);
    }
}
