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
    public class PedidoRepository : IPedidoRepository
    {
        private readonly UStartContext _context;

        public PedidoRepository(UStartContext context)
        {
            _context = context;
        }

        public Pedido ConsultarPorId(Guid id)
        {
            return _context.Pedidos
                .FirstOrDefault(u => u.Id == id);
        }

        public void Add(Pedido Pedido)
        {
            _context.Pedidos.Add(Pedido);
        }

        public void Update(Pedido Pedido)
        {
            _context.Pedidos.Update(Pedido);
        }

        public virtual void Delete(Pedido Pedido)
        {
            if (_context.Entry(Pedido).State == EntityState.Detached)
            {
                _context.Pedidos.Attach(Pedido);
            }
            _context.Pedidos.Remove(Pedido);
        }

        public IEnumerable<Pedido> RetornarTodos()
        {
            return _context
                .Pedidos                
                .ToList();
        }
        public IEnumerable<Pedido> Pesquisar(string pesquisa)
        {
            pesquisa = pesquisa != null ?  pesquisa.ToLower() : "";
            return _context
            .Pedidos
            .Where(x => x.Usuario.Nome.ToLower().Contains(pesquisa))
            .ToList();
        }
    }
}