using System;
using System.Collections.Generic;

namespace UStart.Domain.Commands
{
    public class PedidoCommand
    {
        public Guid Id { get; set; }
        public DateTime DataPedido { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid FormaPagamentoId { get; set; }
        public String Observacao { get; set; }
        public ICollection<PedidoItemCommand> Itens { get; set; }
        public Decimal QuantidadeDeItens { get; set; }
        public Decimal PrecoUnitario { get; set; }
        public Decimal TotalItens { get; set; }
        public Decimal TotalDesconto { get; set; }
        public Decimal TotalProdutos { get; set; }
    }
}