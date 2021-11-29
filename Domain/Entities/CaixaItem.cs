using System;
using UStart.Domain.Commands;

namespace UStart.Domain.Entities
{
    public class CaixaItem
    {
        public Guid Id { get; private set; }
        public Guid CaixaId { get; private set; }
        public Caixa Caixa { get; private set; }
        public Guid ProdutoId { get; private set; }
        public Produto Produto { get; private set; }
        public String Observacao { get; private set; }
        public Decimal Quantidade { get; private set; }
        public Decimal PrecoUnitario { get; private set; }
        public Decimal Desconto { get; private set; }
        public Decimal TotalUnitario { get; private set; }
        public Decimal TotalItem { get; private set; }

        public CaixaItem()
        {
            
        }

        public CaixaItem(CaixaItemCommand command)
        {
            //Id = command.Id == Guid.Empty ? Guid.NewGuid() : command.Id;      
            AtualizarCampos(command);
        }

        public void Update(CaixaItemCommand command)
        {
            AtualizarCampos(command);
        }

        private void AtualizarCampos(CaixaItemCommand command)
        {
            this.Id = command.Id;                    
            this.CaixaId = command.CaixaId;                    
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