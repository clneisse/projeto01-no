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
    public class CaixaItemRepository : ICaixaItemRepository
    {
        private readonly UStartContext _context;

        public CaixaItemRepository(UStartContext context)
        {
            _context = context;
        }

        public CaixaItem ConsultarPorId(Guid id)
        {
            return _context.CaixasItens
                .FirstOrDefault(u => u.Id == id);
        }

        public void Add(CaixaItem CaixaItem)
        {
            _context.CaixasItens.Add(CaixaItem);
        }

        public void Update(CaixaItem CaixaItem)
        {
            _context.CaixasItens.Update(CaixaItem);
        }

        public virtual void Delete(CaixaItem CaixaItem)
        {
            if (_context.Entry(CaixaItem).State == EntityState.Detached)
            {
                _context.CaixasItens.Attach(CaixaItem);
            }
            _context.CaixasItens.Remove(CaixaItem);
        }

        public IEnumerable<CaixaItem> RetornarTodos()
        {
            return _context
                .CaixasItens                
                .ToList();
        }
        
        public IEnumerable<CaixaItem> Pesquisar(string pesquisa)
        {
            pesquisa = pesquisa != null ?  pesquisa.ToLower() : "";
            return _context
            .CaixasItens
            .Where(x => x.Produto.Nome.ToLower().Contains(pesquisa))
            .ToList();
        }
    }
}