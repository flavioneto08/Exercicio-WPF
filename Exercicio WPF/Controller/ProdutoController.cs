using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Exercicio_WPF.Model;

namespace Exercicio_WPF.Controller
{
    public class ProdutoController
    {
        private readonly AppDbContext _context;

        public ProdutoController(AppDbContext context)
        {
            _context = context;
        }

        public List<ProdutoDTO> ListarProdutos()
        {
            var produtos = _context.Produto
                .Include(p => p.Produto_Grupo)
                .OrderBy(p => p.Cod)
                .Select(p => new ProdutoDTO
                {
                    Cod = p.Cod,
                    Descricao = p.Descricao,
                    NomeGrupo = p.Produto_Grupo.Nome,
                    PrecoCusto = p.PrecoCusto,
                    PrecoVenda = p.PrecoVenda,
                }).ToList();

            return produtos;
        }

        public void RemoverProduto(int codProduto)
        {
            var fkProduto = _context.Venda_Produto.Where(o => o.CodProduto == codProduto).ToList(); //Não é a forma "certa" de deletar as chaves estrangeiras, mas por ser um projeto simples resolvi fazer dessa forma
            _context.Venda_Produto.RemoveRange(fkProduto);
            var produto = _context.Produto.FirstOrDefault(p => p.Cod == codProduto);
            if (produto != null)
            {
                _context.Produto.Remove(produto);
                _context.SaveChanges();
            }
        }

        public void AdicionarProduto(ProdutoModel produto)
        {
            _context.Produto.Add(produto);
            _context.SaveChanges();
        }

        public List<Produto_GrupoModel> ListarNomesDosGrupos()
        {
            return _context.Produto_Grupo
                           .Select(g => new Produto_GrupoModel
                           {
                               Cod = g.Cod,
                               Nome = g.Nome,
                           })
                           .ToList();
        }

        public ProdutoModel BuscarProduto(int cod)
        {
            var produto = _context.Produto.FirstOrDefault(p => p.Cod == cod);

            if (produto == null)
            {
                throw new InvalidOperationException($"Produto com o código {cod} não foi encontrado.");
            }

            return produto;
        }

        public bool VerificaProduto(int cod)
        {
            return _context.Produto.Any(p => p.Cod == cod);
        }

        public int CodigoIncremental()
        {
            var ultimoCod = _context.Produto
                .OrderByDescending(p => p.Cod)
                .Select(p => p.Cod)
                .FirstOrDefault();

            return ultimoCod + 1;
        }

        public void AtualizarProduto(ProdutoModel produto)
        {
            var produtoExistente = _context.Produto.FirstOrDefault(p => p.Cod == produto.Cod);

            if (produtoExistente != null)
            {
                produtoExistente.Descricao = produto.Descricao;
                produtoExistente.CodBarra = produto.CodBarra;
                produtoExistente.PrecoCusto = produto.PrecoCusto;
                produtoExistente.PrecoVenda = produto.PrecoVenda;
                produtoExistente.CodGrupo = produto.CodGrupo;
                produtoExistente.Ativo = produto.Ativo;

                _context.SaveChanges();
            }
        }
    }
}
