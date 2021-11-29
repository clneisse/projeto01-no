using System;
using System.Collections.Generic;
using UStart.Domain.Entities;

namespace UStart.Domain.Contracts.Repositories
{
    public interface ICaixaItemRepository
    {
        void Add(CaixaItem CaixaItem);
        CaixaItem ConsultarPorId(Guid id);
        void Delete(CaixaItem CaixaItem);
        IEnumerable<CaixaItem> Pesquisar(string pesquisa);
        IEnumerable<CaixaItem> RetornarTodos();
        void Update(CaixaItem CaixaItem);
    }
}