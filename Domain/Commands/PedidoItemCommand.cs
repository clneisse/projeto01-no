using System;
using System.Collections.Generic;

namespace UStart.Domain.Commands
{
    public class PedidoItemCommand
    {
        public Guid Id { get; set; }
        public Guid PedidoId { get; set; }
        public Guid ProdutoId { get; set; }
        public String Observacao { get; set; }
        public Decimal Quantidade { get; set; }
        public Decimal PrecoUnitario { get; set; }
        public Decimal Desconto { get; set; }
        public Decimal TotalUnitario { get; set; }
        public Decimal TotalItem { get; set; }
    }
}