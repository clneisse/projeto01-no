using System;
using System.Collections.Generic;

namespace UStart.Domain.Commands
{
    public class CaixaCommand
    {
        public Guid Id { get; set; }
        public DateTime DataCaixa { get; set; }
        public Guid? UsuarioId { get; set; }        
        public Guid FormaPagamentoId { get; set; }        
        public String Observacao { get; set; }
        public IList<CaixaItemCommand> Itens { get; set; }
        public Decimal QuantidadeDeItens { get; set; }        
        public Decimal TotalItens { get; set; }
        public Decimal TotalDesconto { get; set; }
        public Decimal TotalProdutos { get; set; }

        public CaixaCommand()
        {
            this.Itens = new List<CaixaItemCommand>();
        }
    }
}