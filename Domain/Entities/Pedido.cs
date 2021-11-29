using System;
using System.Collections.Generic;
using System.Linq;
using UStart.Domain.Commands;

namespace UStart.Domain.Entities
{
    public class Pedido
    {
        public Guid Id { get; private set; }
        public DateTime DataPedido { get; private set; }
        public Guid UsuarioId { get; private set; }
        public Usuario Usuario { get; private set; }
        public Guid FormaPagamentoId { get; private set; }
        public FormaPagamento FormaPagamento { get; private set; }
        public String Observacao { get; private set; }
        public ICollection<PedidoItem> Itens { get; private set; }
        public Decimal QuantidadeDeItens { get; private set; }
        public Decimal PrecoUnitario { get; private set; }
        public Decimal TotalItens { get; private set; }
        public Decimal TotalDesconto { get; private set; }
        public Decimal TotalProdutos { get; private set; }

        public Pedido()
        {
        }

        public Pedido(PedidoCommand command)
        {
            Id = command.Id == Guid.Empty ? Guid.NewGuid() : command.Id;      

            this.Itens = command.Itens
                .Select(itemCmd => new PedidoItem(itemCmd))
                .ToList();    

            AtualizarCampos(command);
        }

        public void Update(PedidoCommand command)
        {
            //Remover os items da entidade que não vierem no command
            var itensParaExcluir = this.Itens
                .Where(c => !command.Itens.Any(itemCmd => itemCmd.Id == c.Id))
                .ToList();

            foreach (var item in itensParaExcluir)
            {
                this.Itens.Remove(item);
            }

            //Atualizar os itens que vierem no command
            var itensParaAtualizar = this.Itens
                .Where(c => command.Itens.Any(itemCmd => itemCmd.Id == c.Id))
                .ToList();
            foreach (var item in itensParaAtualizar)
            {
                var itemCmd = command.Itens.FirstOrDefault(c => c.Id == item.Id);
                if (itemCmd != null)
                {
                    item.Update(itemCmd);
                }
            }

            //Adicionar os novos itens que vierem pelo command
            var itensParaAdicionar = command.Itens
                .Where(itemCmd => !this.Itens.Any(item => item.Id == itemCmd.Id)).ToList();

            foreach (var itemCmd in itensParaAdicionar)
            {                
                itemCmd.Id = itemCmd.Id == Guid.Empty ? Guid.NewGuid() : itemCmd.Id;                 
                Itens.Add(new PedidoItem(itemCmd));
            }

            //Atualiza os campos da entidade e totais
            AtualizarCampos(command);
        }

        private void AtualizarCampos(PedidoCommand command)
        {
            this.Id = command.Id;                    
            this.DataPedido = command.DataPedido;                                  
            this.UsuarioId = command.UsuarioId;                    
            this.FormaPagamentoId = command.FormaPagamentoId;    
            
            
            //Atualizar cálculos
            //--------------------------------
            //Quantidade de itens do orçamento
            this.QuantidadeDeItens = this.Itens.Count;
            //Preço unitário x quantidade
            this.TotalItens = this.Itens.Sum(x => x.PrecoUnitario * x.Quantidade);
            //Desconto unitário x quantidade 
            this.TotalDesconto = this.Itens.Sum(x => x.Desconto * x.Quantidade); 
            //Total dos produtos TotalItens - TotalDesconto;
            this.TotalProdutos = this.TotalItens - this.TotalDesconto;

            
        }

    }
}