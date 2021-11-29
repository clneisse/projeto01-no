using System;
using System.Collections.Generic;
using UStart.Domain.Entities;

namespace UStart.Domain.Contracts.Repositories
{
    public interface IPedidoRepository
    {
        void Add(Pedido Pedido);
        Pedido ConsultarPorId(Guid id);
        void Delete(Pedido Pedido);
        IEnumerable<Pedido> Pesquisar(string pesquisa);
        IEnumerable<Pedido> RetornarTodos();
        void Update(Pedido Pedido);
    }
}