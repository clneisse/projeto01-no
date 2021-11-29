using System;
using System.Collections.Generic;
using System.Linq;
using UStart.Domain.Entities;

namespace UStart.Domain.Results
{
    public class OrcamentoResult
    {
        public Guid Id { get; set; }
        public DateTime DataOrcamento { get; set; }
        public Guid UsuarioId { get; set; }
        public UsuarioResult Usuario { get; set; }
        public Guid FormaPagamentoId { get; set; }
        public FormaPagamento FormaPagamento { get; set; }
        public String Observacao { get; set; }
        public Decimal QuantidadeDeItens { get; set; }
        public Decimal TotalItens { get; set; }
        public Decimal TotalDesconto { get; set; }
        public Decimal TotalProdutos { get; set; }
        public ICollection<OrcamentoItemResult> Itens { get; set; }

        public OrcamentoResult(Orcamento orcamento)
        {
            if (orcamento == null)
            {
                return;
            }

            this.Id = orcamento.Id;
            this.DataOrcamento = orcamento.DataOrcamento;
            this.UsuarioId = orcamento.UsuarioId;
            this.Usuario = new UsuarioResult(orcamento.Usuario);
            this.FormaPagamentoId = orcamento.FormaPagamentoId;
            this.FormaPagamento = orcamento.FormaPagamento;
            this.Observacao = orcamento.Observacao;
            this.QuantidadeDeItens = orcamento.QuantidadeDeItens;
            this.TotalItens = orcamento.TotalItens;
            this.TotalDesconto = orcamento.TotalDesconto;
            this.TotalProdutos = orcamento.TotalProdutos;

            if (orcamento.Itens != null)
            {
                this.Itens = orcamento.Itens
                    .Select(item => new OrcamentoItemResult(item))
                    .ToList();
            }
        }
    }
}