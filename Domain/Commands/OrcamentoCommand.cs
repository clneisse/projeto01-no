using System;
using System.Collections.Generic;

namespace UStart.Domain.Commands
{
    public class OrcamentoCommand
    {
        public Guid Id { get; set; }
        public DateTime DataOrcamento { get; set; }
        public Guid? UsuarioId { get; set; }        
        public Guid FormaPagamentoId { get; set; }        
        public String Observacao { get; set; }
        public IList<OrcamentoItemCommand> Itens { get; set; }
        public Decimal QuantidadeDeItens { get; set; }        
        public Decimal TotalItens { get; set; }
        public Decimal TotalDesconto { get; set; }
        public Decimal TotalProdutos { get; set; }

        public OrcamentoCommand()
        {
            this.Itens = new List<OrcamentoItemCommand>();
        }
    }
}