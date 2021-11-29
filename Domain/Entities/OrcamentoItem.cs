using System;
using UStart.Domain.Commands;

namespace UStart.Domain.Entities
{
    public class OrcamentoItem
    {
        public Guid Id { get; private set; }
        public Guid OrcamentoId { get; private set; }
        public Orcamento Orcamento { get; private set; }
        public Guid ProdutoId { get; private set; }
        public Produto Produto { get; private set; }
        public String Observacao { get; private set; }
        public Decimal Quantidade { get; private set; }
        public Decimal PrecoUnitario { get; private set; }
        public Decimal Desconto { get; private set; }
        public Decimal TotalUnitario { get; private set; }
        public Decimal TotalItem { get; private set; }

        public OrcamentoItem()
        {
            
        }

        public OrcamentoItem(OrcamentoItemCommand command)
        {
            //Id = command.Id == Guid.Empty ? Guid.NewGuid() : command.Id;      
            AtualizarCampos(command);
        }

        public void Update(OrcamentoItemCommand command)
        {
            AtualizarCampos(command);
        }

        private void AtualizarCampos(OrcamentoItemCommand command)
        {
            this.Id = command.Id;                    
            this.OrcamentoId = command.OrcamentoId;                    
            this.ProdutoId = command.ProdutoId;                    
            this.Observacao = command.Observacao;                    
            this.Quantidade = command.Quantidade;                    
            this.PrecoUnitario = command.PrecoUnitario;                    
            this.Desconto = command.Desconto;                    
            this.TotalUnitario = command.TotalUnitario;                    
            this.TotalItem = command.TotalItem;                                                       
        }
    }
}