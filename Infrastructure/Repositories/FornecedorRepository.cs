using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using UStart.Domain.Contracts.Repositories;
using UStart.Domain.Entities;
using UStart.Infrastructure.Context;

namespace UStart.Infrastructure.Repositories
{
    public class FornecedorRepository : IFornecedorRepository
    {
        private readonly UStartContext _context;

        public FornecedorRepository(UStartContext context)
        {
            _context = context;
        }

        public Fornecedor ConsultarPorId(Guid id)
        {
            return _context.Fornecedor.FirstOrDefault(g => g.Id == id);
        }

        public IEnumerable<Fornecedor> Pesquisar(string pesquisa)
        {
            pesquisa = pesquisa != null ? pesquisa.ToLower() : string.Empty;

            return _context
            .Fornecedores
            .Where(x => x.Nome.ToLower().Contains(pesquisa))
            .ToList();
        }

        public void Add(Fornecedor Fornecedor)
        {
            _context.Fornecedores.Add(Fornecedor);
        }

        public void Update(Fornecedor Fornecedor)
        {
            _context.Fornecedores.Update(Fornecedor);
        }

        public void Delete(Fornecedor Fornecedor)
        {
            if (_context.Entry(Fornecedor).State == EntityState.Detached)
            {
                _context.Fornecedores.Attach(Fornecedor);
            }
            _context.Fornecedores.Remove(Fornecedor);
        }
    }
}