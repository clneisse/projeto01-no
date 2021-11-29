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
    public class FormaPagamentoRepository : IFormaPagamentoRepository
    {
        private readonly UStartContext _context;

        public FormaPagamentoRepository(UStartContext context)
        {
            _context = context;
        }

        public FormaPagamento ConsultarPorId(Guid id)
        {
            return _context.FormasPagamentos
                .FirstOrDefault(u => u.Id == id);
        }

        public void Add(FormaPagamento FormaPagamento)
        {
            _context.FormasPagamentos.Add(FormaPagamento);
        }

        public void Update(FormaPagamento FormaPagamento)
        {
            _context.FormasPagamentos.Update(FormaPagamento);
        }

        public virtual void Delete(FormaPagamento FormaPagamento)
        {
            if (_context.Entry(FormaPagamento).State == EntityState.Detached)
            {
                _context.FormasPagamentos.Attach(FormaPagamento);
            }
            _context.FormasPagamentos.Remove(FormaPagamento);
        }

        public IEnumerable<FormaPagamento> RetornarTodos()
        {
            return _context
                .FormasPagamentos                
                .ToList();
        }
        public IEnumerable<FormaPagamento> Pesquisar(string pesquisa)
        {
            pesquisa = pesquisa != null ?  pesquisa.ToLower() : "";
            return _context
            .FormasPagamentos
            .Where(x => x.Descricao.ToLower().Contains(pesquisa))
            .ToList();
        }
    }
}