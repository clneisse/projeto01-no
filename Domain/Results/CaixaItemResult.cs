using System;
using UStart.Domain.Entities;

namespace UStart.Domain.Results
{
    public class CaixaItemResult
    {
        public Guid Id { get; set; }
        public Guid CaixaId { get; set; }        
        public Guid ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public String Observacao { get; set; }
        public Decimal Quantidade { get; set; }
        public Decimal PrecoUnitario { get; set; }
        public Decimal Desconto { get; set; }
        public Decimal TotalUnitario { get; set; }
        public Decimal TotalItem { get; set; }

        public CaixaItemResult(CaixaItem caixaItem)
        {
            if (caixaItem == null){
                return;
            }
            
            this.Id = caixaItem.Id;                    
            this.CaixaId = caixaItem.CaixaId;                    
            this.ProdutoId = caixaItem.ProdutoId;                    
            this.Produto = caixaItem.Produto;
            this.Observacao = caixaItem.Observacao;                    
            this.Quantidade = caixaItem.Quantidade;                    
            this.PrecoUnitario = caixaItem.PrecoUnitario;                    
            this.Desconto = caixaItem.Desconto;                    
            this.TotalUnitario = caixaItem.TotalUnitario;                    
            this.TotalItem = caixaItem.TotalItem;  
        }
    }
}