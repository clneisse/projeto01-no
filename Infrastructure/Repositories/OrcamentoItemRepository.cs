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
    public class OrcamentoItemRepository : IOrcamentoItemRepository
    {
        private readonly UStartContext _context;

        public OrcamentoItemRepository(UStartContext context)
        {
            _context = context;
        }

        public OrcamentoItem ConsultarPorId(Guid id)
        {
            return _context.OrcamentosItens
                .FirstOrDefault(u => u.Id == id);
        }

        public void Add(OrcamentoItem OrcamentoItem)
        {
            _context.OrcamentosItens.Add(OrcamentoItem);
        }

        public void Update(OrcamentoItem OrcamentoItem)
        {
            _context.OrcamentosItens.Update(OrcamentoItem);
        }

        public virtual void Delete(OrcamentoItem OrcamentoItem)
        {
            if (_context.Entry(OrcamentoItem).State == EntityState.Detached)
            {
                _context.OrcamentosItens.Attach(OrcamentoItem);
            }
            _context.OrcamentosItens.Remove(OrcamentoItem);
        }

        public IEnumerable<OrcamentoItem> RetornarTodos()
        {
            return _context
                .OrcamentosItens                
                .ToList();
        }
        
        public IEnumerable<OrcamentoItem> Pesquisar(string pesquisa)
        {
            pesquisa = pesquisa != null ?  pesquisa.ToLower() : "";
            return _context
            .OrcamentosItens
            .Where(x => x.Produto.Nome.ToLower().Contains(pesquisa))
            .ToList();
        }
    }
}