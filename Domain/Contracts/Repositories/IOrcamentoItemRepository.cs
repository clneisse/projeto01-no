using System;
using System.Collections.Generic;
using UStart.Domain.Entities;

namespace UStart.Domain.Contracts.Repositories
{
    public interface IOrcamentoItemRepository
    {
        void Add(OrcamentoItem OrcamentoItem);
        OrcamentoItem ConsultarPorId(Guid id);
        void Delete(OrcamentoItem OrcamentoItem);
        IEnumerable<OrcamentoItem> Pesquisar(string pesquisa);
        IEnumerable<OrcamentoItem> RetornarTodos();
        void Update(OrcamentoItem OrcamentoItem);
    }
}