using System;
using UStart.Domain.Entities;

namespace UStart.Domain.Results
{
    public class OrcamentoItemResult
    {
        public Guid Id { get; set; }
        public Guid OrcamentoId { get; set; }        
        public Guid ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public String Observacao { get; set; }
        public Decimal Quantidade { get; set; }
        public Decimal PrecoUnitario { get; set; }
        public Decimal Desconto { get; set; }
        public Decimal TotalUnitario { get; set; }
        public Decimal TotalItem { get; set; }

        public OrcamentoItemResult(OrcamentoItem orcamentoItem)
        {
            if (orcamentoItem == null){
                return;
            }
            
            this.Id = orcamentoItem.Id;                    
            this.OrcamentoId = orcamentoItem.OrcamentoId;                    
            this.ProdutoId = orcamentoItem.ProdutoId;                    
            this.Produto = orcamentoItem.Produto;
            this.Observacao = orcamentoItem.Observacao;                    
            this.Quantidade = orcamentoItem.Quantidade;                    
            this.PrecoUnitario = orcamentoItem.PrecoUnitario;                    
            this.Desconto = orcamentoItem.Desconto;                    
            this.TotalUnitario = orcamentoItem.TotalUnitario;                    
            this.TotalItem = orcamentoItem.TotalItem;  
        }
    }
}