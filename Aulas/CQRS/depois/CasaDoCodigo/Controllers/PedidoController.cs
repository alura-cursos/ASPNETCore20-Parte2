using CasaDoCodigo.Models;
using CasaDoCodigo.Models.ViewModels;
using CasaDoCodigo.Queries;
using CasaDoCodigo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IProdutoRepository produtoRepository;
        private readonly IPedidoRepository pedidoRepository;
        private readonly IItemPedidoRepository itemPedidoRepository;
        private readonly IPedidoQueries pedidoQueries;
        private readonly IHttpContextAccessor contextAccessor;

        public PedidoController
            (IHttpContextAccessor contextAccessor,
            IProdutoRepository produtoRepository,
            IPedidoRepository pedidoRepository,
            IItemPedidoRepository itemPedidoRepository,
            IPedidoQueries pedidoQueries)
        {
            this.contextAccessor = contextAccessor;
            this.produtoRepository = produtoRepository;
            this.pedidoRepository = pedidoRepository;
            this.itemPedidoRepository = itemPedidoRepository;
            this.pedidoQueries = pedidoQueries;
        }

        private int? GetPedidoId()
        {
            return contextAccessor.HttpContext.Session.GetInt32("pedidoId");
        }

        public async Task<IActionResult> Carrossel()
        {
            return View(await produtoRepository.GetProdutos());
        }

        public async Task<IActionResult> Carrinho(string codigo)
        {
            if (!string.IsNullOrEmpty(codigo))
            {
                await pedidoRepository.AddItem(codigo);
            }

            CarrinhoViewModel carrinhoViewModel = await pedidoQueries.GetCarrinho(GetPedidoId().Value);

            //Pedido pedido = await pedidoRepository.GetPedido();
            //List<ItemPedido> itens = pedido.Itens;
            //CarrinhoViewModel carrinhoViewModel = new CarrinhoViewModel(itens);
            return base.View(carrinhoViewModel);
        }

        public async Task<IActionResult> Cadastro()
        {
            var pedido = await pedidoRepository.GetPedido();

            if (pedido == null)
            {
                return RedirectToAction("Carrossel");
            }

            return View(pedido.Cadastro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Resumo(Cadastro cadastro)
        {
            if (ModelState.IsValid)
            {
                return View(await pedidoRepository.UpdateCadastro(cadastro));
            }
            return RedirectToAction("Cadastro");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<UpdateQuantidadeResponse> UpdateQuantidade([FromBody]ItemPedido itemPedido)
        {
            return await pedidoRepository.UpdateQuantidade(itemPedido);
        }
    }
}
