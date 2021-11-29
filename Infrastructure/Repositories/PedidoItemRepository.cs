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
    public class PedidoItemRepository : IPedidoItemRepository
    {
        private readonly UStartContext _context;

        public PedidoItemRepository(UStartContext context)
        {
            _context = context;
        }

        public PedidoItem ConsultarPorId(Guid id)
        {
            return _context.PedidosItens
                .FirstOrDefault(u => u.Id == id);
        }

        public void Add(PedidoItem PedidoItem)
        {
            _context.PedidosItens.Add(PedidoItem);
        }

        public void Update(PedidoItem PedidoItem)
        {
            _context.PedidosItens.Update(PedidoItem);
        }

        public virtual void Delete(PedidoItem PedidoItem)
        {
            if (_context.Entry(PedidoItem).State == EntityState.Detached)
            {
                _context.PedidosItens.Attach(PedidoItem);
            }
            _context.PedidosItens.Remove(PedidoItem);
        }

        public IEnumerable<PedidoItem> RetornarTodos()
        {
            return _context
                .PedidosItens                
                .ToList();
        }
        public IEnumerable<PedidoItem> Pesquisar(string pesquisa)
        {
            pesquisa = pesquisa != null ?  pesquisa.ToLower() : "";
            return _context
            .PedidosItens
            .Where(x => x.Produto.Nome.ToLower().Contains(pesquisa))
            .ToList();
        }
    }
}