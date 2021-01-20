using System;
using System.Collections.Generic;
using System.Linq;
using EFcore.Domain;
using EFcore.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace EFcore
{
    class Program
    {
        static void Main(string[] args)
        {
            // InserirDados();
            // InserirDadosEmMassa();
            // ConsultarDados();
            // CadastrarPedido();
            ConsultaPedidoCarregamentoAdiantado();
        }

        private static void InserirDadosEmMassa()
        {
            var produto = new Produto
            {
                Descricao = "Produto teste",
                CodigoBarras = "1234567891231",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaVenda,
                Ativo = true
            };

            var Cliente = new Cliente
            {
                Nome = "Rafael Almeida",
                CEP = "9999999",
                Cidade = "Paraíso",
                Estado = "TO",
                Telefone = "63999999999",
            };

            using var db = new Data.ApplicationContext();
            db.AddRange(produto, Cliente);
            var registros = db.SaveChanges();
            Console.WriteLine($"registros = {registros}");
        }

        private static void InserirDados()
        {
            var produto = new Produto
            {
                Descricao = "Produto teste",
                CodigoBarras = "1234567891231",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaVenda,
                Ativo = true
            };

            using var db = new Data.ApplicationContext();
            // db.Produtos.Add(produto);
            // db.Set<Produto>().Add(produto);
            // db.Entry(produto).State = EntityState.Added;
            db.Add(produto);

            var registros = db.SaveChanges();
            Console.WriteLine($"Toral Registros: {registros}");

        }

        private static void CadastrarPedido()
        {
            using var db = new Data.ApplicationContext();
            var cliente = db.Clientes.FirstOrDefault();
            var produto = db.Produtos.FirstOrDefault();

            var pedido = new Pedido
            {
                ClienteId = cliente.Id,
                IniciadoEm = DateTime.Now,
                FinalizadoEm = DateTime.Now,
                Observacao = "pedido teste",
                Status = StatusPedido.Analise,
                TipoFrete = TipoFrete.SemFrete,
                Itens = new List<PedidoItem>
                {
                    new PedidoItem
                    {
                        ProdutoId = produto.Id,
                        Desconto = 0,
                        Quantidade = 1,
                        Valor = 10,
                    }
                }

            };

            db.Add(pedido);
            db.SaveChanges();
        }

        private static void ConsultarDados()
        {
            using var db = new Data.ApplicationContext();
            // var consultaPorSintaxe = (from c in db.Clientes where c.Id > 0 select c).ToList();
            var consultaPorMedotodo = db.Clientes.Where(p => p.Id > 0).ToList();

            foreach (var cliente in consultaPorMedotodo)
            {
                Console.WriteLine($"Consultando Cliente: {cliente.Id}");
                // db.Clientes.Find(cliente.Id);
                db.Clientes.FirstOrDefault(p => p.Id == cliente.Id);
            }
        }

        private static void ConsultaPedidoCarregamentoAdiantado()
        {
            using var db = new Data.ApplicationContext();
            var pedidos = db.Pedidos.Include(p => p.Itens).ThenInclude(p => p.Produto).ToList();
            Console.WriteLine(pedidos.Count);
        }
    }
}
