using System;
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
            InserirDadosEmMassa();
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
    }
}
