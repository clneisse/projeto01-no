using System;
using System.Collections.Generic;
using UStart.Domain.Entities;

namespace UStart.Domain.Contracts.Repositories
{
    public interface IPedidoItemRepository
    {
        void Add(PedidoItem PedidoItem);
        PedidoItem ConsultarPorId(Guid id);
        void Delete(PedidoItem PedidoItem);
        IEnumerable<PedidoItem> Pesquisar(string pesquisa);
        IEnumerable<PedidoItem> RetornarTodos();
        void Update(PedidoItem PedidoItem);
    }
}