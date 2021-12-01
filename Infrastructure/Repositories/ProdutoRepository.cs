using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using UStart.Domain.Contracts.Entities;
using UStart.Domain.Contracts.Repositories;
using UStart.Domain.Entities;
using UStart.Domain.Results;
using UStart.Infrastructure.Context;

namespace UStart.Infrastructure.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly UStartContext _context;

        public ProdutoRepository(UStartContext context)
        {
            _context = context;
        }

        public Produto ConsultarPorId(Guid id)
        {
            return _context.Produtos
                .FirstOrDefault(u => u.Id == id);
        }

        public void Add(Produto Produto)
        {
            _context.Produtos.Add(Produto);
        }

        public void Update(Produto Produto)
        {
            _context.Produtos.Update(Produto);
        }

        public virtual void Delete(Produto Produto)
        {
            if (_context.Entry(Produto).State == EntityState.Detached)
            {
                _context.Produtos.Attach(Produto);
            }
            _context.Produtos.Remove(Produto);
        }

        public IEnumerable<Produto> RetornarTodos()
        {
            return _context
                .Produtos
                .Include(x => x.GrupoProduto)
                .ToList();
        }
        public IEnumerable<Produto> Pesquisar(string pesquisa)
        {
            pesquisa = pesquisa != null ?  pesquisa.ToLower() : "";
            return _context
            .Produtos
            .Where(x => x.Descricao.ToLower().Contains(pesquisa))
            .ToList();
        }
    }
}