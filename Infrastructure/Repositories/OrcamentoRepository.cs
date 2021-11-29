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
    public class OrcamentoRepository : IOrcamentoRepository
    {
        private readonly UStartContext _context;

        public OrcamentoRepository(UStartContext context)
        {
            _context = context;
        }

        public Orcamento ConsultarPorId(Guid id)
        {
            return _context
                .Orcamentos
                .Include(x => x.Itens)
                .FirstOrDefault(u => u.Id == id);
        }
        public OrcamentoResult GetOrcamentoResultPorId(Guid id)
        {
            Orcamento orcamento = _context
                .Orcamentos
                .Include(u => u.Usuario)
                .Include(f => f.FormaPagamento)
                .Include(i => i.Itens)
                    .ThenInclude(p => p.Produto)                    
                .FirstOrDefault(u => u.Id == id);
            if (orcamento == null)
            {
                return null;
            }
            return new OrcamentoResult(orcamento);
        }

        public void Add(Orcamento Orcamento)
        {
            _context.Orcamentos.Add(Orcamento);
        }

        public void Update(Orcamento Orcamento)
        {
            _context.Orcamentos.Update(Orcamento);
        }

        public virtual void Delete(Orcamento Orcamento)
        {
            if (_context.Entry(Orcamento).State == EntityState.Detached)
            {
                _context.Orcamentos.Attach(Orcamento);
            }
            _context.Orcamentos.Remove(Orcamento);
        }

        public IEnumerable<Orcamento> RetornarTodos()
        {
            return _context
                .Orcamentos
                .ToList();
        }
        public IEnumerable<OrcamentoResult> Pesquisar(string pesquisa)
        {
            pesquisa = pesquisa != null ? pesquisa.ToLower() : "";
            return _context
            .Orcamentos
            .Include(f => f.FormaPagamento)
            .Where(x => x.Usuario.Nome.ToLower().Contains(pesquisa))
            .Select(o => new OrcamentoResult(o))
            .ToList();
        }

        public IEnumerable<dynamic> ConsultarTotaisOrcamento(DateTime dtInicial, DateTime dtFinal)
        {
            dtInicial = new DateTime(dtInicial.Year, dtInicial.Month, dtInicial.Day, 0, 0, 0, 0);
            dtFinal = new DateTime(dtFinal.Year, dtFinal.Month, dtFinal.Day, 23, 59, 59, 999);


            return _context
                .Orcamentos
                //.Where(x => x.ClienteId = "asd")
                .Where(x => x.DataOrcamento >= dtInicial && x.DataOrcamento <= dtFinal)
                .ToList()
                .GroupBy(or => or)
                .Select(or => new
                {
                    totalDesconto = or.Sum(group => group.TotalDesconto),
                    totalPedidos = or.Sum(group => group.TotalProdutos)
                });
        }
    }
}