using System;
using System.Collections.Generic;
using UStart.Domain.Entities;

namespace UStart.Domain.Contracts.Repositories
{
    public interface IFormaPagamentoRepository
    {
        void Add(FormaPagamento FormaPagamento);
        FormaPagamento ConsultarPorId(Guid id);
        void Delete(FormaPagamento FormaPagamento);
        IEnumerable<FormaPagamento> Pesquisar(string pesquisa);
        IEnumerable<FormaPagamento> RetornarTodos();
        void Update(FormaPagamento FormaPagamento);
    }
}