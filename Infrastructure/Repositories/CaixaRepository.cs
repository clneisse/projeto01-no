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
    public class CaixaRepository : ICaixaRepository
    {
        private readonly UStartContext _context;

        public CaixaRepository(UStartContext context)
        {
            _context = context;
        }

        public Caixa ConsultarPorId(Guid id)
        {
            return _context
                .Caixas
                .Include(x => x.Itens)
                .FirstOrDefault(u => u.Id == id);
        }
        public CaixaResult GetCaixaResultPorId(Guid id)
        {
            Caixa caixa = _context
                .Caixas
                .Include(u => u.Usuario)
                .Include(f => f.FormaPagamento)
                .Include(i => i.Itens)
                    .ThenInclude(p => p.Produto)                    
                .FirstOrDefault(u => u.Id == id);
            if (caixa == null)
            {
                return null;
            }
            return new CaixaResult(caixa);
        }

        public void Add(Caixa Caixa)
        {
            _context.Caixas.Add(Caixa);
        }

        public void Update(Caixa Caixa)
        {
            _context.Caixas.Update(Caixa);
        }

        public virtual void Delete(Caixa Caixa)
        {
            if (_context.Entry(Caixa).State == EntityState.Detached)
            {
                _context.Caixas.Attach(Caixa);
            }
            _context.Caixas.Remove(Caixa);
        }

        public IEnumerable<Caixa> RetornarTodos()
        {
            return _context
                .Caixas
                .ToList();
        }
        public IEnumerable<CaixaResult> Pesquisar(string pesquisa)
        {
            pesquisa = pesquisa != null ? pesquisa.ToLower() : "";
            return _context
            .Caixas
            .Include(f => f.FormaPagamento)
            .Where(x => x.Usuario.Nome.ToLower().Contains(pesquisa))
            .Select(o => new CaixaResult(o))
            .ToList();
        }

        public IEnumerable<dynamic> ConsultarTotaisCaixa(DateTime dtInicial, DateTime dtFinal)
        {
            dtInicial = new DateTime(dtInicial.Year, dtInicial.Month, dtInicial.Day, 0, 0, 0, 0);
            dtFinal = new DateTime(dtFinal.Year, dtFinal.Month, dtFinal.Day, 23, 59, 59, 999);


            return _context
                .Caixas
                //.Where(x => x.ClienteId = "asd")
                .Where(x => x.DataCaixa >= dtInicial && x.DataCaixa <= dtFinal)
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