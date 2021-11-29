using System;
using System.Collections.Generic;
using System.Linq;
using UStart.Domain.Entities;

namespace UStart.Domain.Results
{
    public class CaixaResult
    {
        public Guid Id { get; set; }
        public DateTime DataCaixa { get; set; }
        public Guid UsuarioId { get; set; }
        public UsuarioResult Usuario { get; set; }
        public Guid FormaPagamentoId { get; set; }
        public FormaPagamento FormaPagamento { get; set; }
        public String Observacao { get; set; }
        public Decimal QuantidadeDeItens { get; set; }
        public Decimal TotalItens { get; set; }
        public Decimal TotalDesconto { get; set; }
        public Decimal TotalProdutos { get; set; }
        public ICollection<CaixaItemResult> Itens { get; set; }

        public CaixaResult(Caixa caixa)
        {
            if (caixa == null)
            {
                return;
            }

            this.Id = caixa.Id;
            this.DataCaixa = caixa.DataCaixa;
            this.UsuarioId = caixa.UsuarioId;
            this.Usuario = new UsuarioResult(caixa.Usuario);
            this.FormaPagamentoId = caixa.FormaPagamentoId;
            this.FormaPagamento = caixa.FormaPagamento;
            this.Observacao = caixa.Observacao;
            this.QuantidadeDeItens = caixa.QuantidadeDeItens;
            this.TotalItens = caixa.TotalItens;
            this.TotalDesconto = caixa.TotalDesconto;
            this.TotalProdutos = caixa.TotalProdutos;

            if (caixa.Itens != null)
            {
                this.Itens = caixa.Itens
                    .Select(item => new CaixaItemResult(item))
                    .ToList();
            }
        }
    }
}