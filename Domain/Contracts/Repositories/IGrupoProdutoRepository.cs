using System;
using System.Collections.Generic;
using UStart.Domain.Entities;

namespace UStart.Domain.Contracts.Repositories
{
    public interface IGrupoProdutoRepository
    {
        void Add(GrupoProduto grupoProduto);
        GrupoProduto ConsultarPorId(Guid id);
        IEnumerable<GrupoProduto> Pesquisar(string pesquisa);
    }
}