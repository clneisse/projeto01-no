using System;
using System.Collections.Generic;
using UStart.Domain.Entities;
using UStart.Domain.Results;

namespace UStart.Domain.Contracts.Repositories
{
    public interface IOrcamentoRepository
    {
        void Add(Orcamento Orcamento);
        Orcamento ConsultarPorId(Guid id);
        OrcamentoResult GetOrcamentoResultPorId(Guid id);
        void Delete(Orcamento Orcamento);
        IEnumerable<OrcamentoResult> Pesquisar(string pesquisa);
        IEnumerable<Orcamento> RetornarTodos();
        void Update(Orcamento Orcamento);
        IEnumerable<dynamic> ConsultarTotaisOrcamento(DateTime dtInicial, DateTime dtFinal);
    }
}