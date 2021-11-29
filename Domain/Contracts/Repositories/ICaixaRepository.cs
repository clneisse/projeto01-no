using System;
using System.Collections.Generic;
using UStart.Domain.Entities;
using UStart.Domain.Results;

namespace UStart.Domain.Contracts.Repositories
{
    public interface ICaixaRepository
    {
        void Add(Caixa Caixa);
        Caixa ConsultarPorId(Guid id);
        CaixaResult GetCaixaResultPorId(Guid id);
        void Delete(Caixa Caixa);
        IEnumerable<CaixaResult> Pesquisar(string pesquisa);
        IEnumerable<Caixa> RetornarTodos();
        void Update(Caixa Caixa);
        IEnumerable<dynamic> ConsultarTotaisCaixa(DateTime dtInicial, DateTime dtFinal);
    }
}