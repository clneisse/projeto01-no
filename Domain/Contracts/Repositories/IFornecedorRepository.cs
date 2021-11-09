using System;
using System.Collections.Generic;
using UStart.Domain.Entities;

namespace UStart.Domain.Contracts.Repositories
{
    public interface IFornecedorRepository
    {
        void Add(Fornecedor Fornecedor);
        Fornecedor ConsultarPorId(Guid id);
        void Delete(Fornecedor Fornecedor);
        IEnumerable<Fornecedor> Pesquisar(string pesquisa);
        void Update(Fornecedor Fornecedor);
    }
}