using System;
using UStart.Domain.Commands;

namespace UStart.Domain.Entities
{
    public class PedidoItem
    {
        public Guid Id { get; private set; }
        public Guid PedidoId { get; private set; }
        public Pedido Pedido { get; private set; }
        public Guid ProdutoId { get; private set; }
        public Produto Produto { get; private set; }
        public String Observacao { get; private set; }
        public Decimal Quantidade { get; private set; }
        public Decimal PrecoUnitario { get; private set; }
        public Decimal Desconto { get; private set; }
        public Decimal TotalUnitario { get; private set; }
        public Decimal TotalItem { get; private set; }

        public PedidoItem()
        {
            
        }

        public PedidoItem(PedidoItemCommand command)
        {
            Id = command.Id == Guid.Empty ? Guid.NewGuid() : command.Id;      
            AtualizarCampos(command);
        }

        public void Update(PedidoItemCommand command)
        {
            AtualizarCampos(command);
        }

        private void AtualizarCampos(PedidoItemCommand command)
        {
            this.Id = command.Id;                    
            this.PedidoId = command.PedidoId;
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