using UStart.Domain.Commands;
using UStart.Domain.Entities;
using Xunit;
using System.Linq;

namespace UStart.Tests
{
    public class Orcamento_Test
    {
        public Orcamento_Test()
        {
            
        }

        [Fact(DisplayName = "Teste criar novo orçamento")]        
        public void Novo_Orcamento_Test()
        {
            OrcamentoCommand command = new OrcamentoCommand();
            command.Itens.Add(new OrcamentoItemCommand(){
                Quantidade = 2,
                Desconto = 10,
                PrecoUnitario = 10            
            });
                
            Orcamento orcamento = new Orcamento(command);

            Assert.Equal(orcamento.Itens.Count, command.Itens.Count);
            Assert.Equal(orcamento.QuantidadeDeItens, command.Itens.Count);            
        }

        [Theory(DisplayName = "Calcular totais e desconto")]
        [InlineData(2,10,10, 18)]
        [InlineData(10,10,10, 90)]
        [InlineData(10,0,10, 100)]
        [InlineData(0,10,10, 0)]
        public void Orcamento_calcular_totais_Test(decimal quantidade, decimal desconto, decimal precoUni, decimal exptected)
        {
            OrcamentoCommand command = new OrcamentoCommand();
            command.Itens.Add(new OrcamentoItemCommand(){
                Quantidade = quantidade,
                Desconto = desconto,
                PrecoUnitario = precoUni           
            });

            var totalDesconto = command.Itens.Sum(x => x.Quantidade * x.PrecoUnitario * (x.Desconto /100));
                    
            Orcamento orcamento = new Orcamento(command);

            Assert.Equal(orcamento.Itens.Count, command.Itens.Count);
            Assert.Equal(orcamento.QuantidadeDeItens, command.Itens.Count);
            Assert.Equal(orcamento.TotalDesconto, totalDesconto);  

            var totalItens = command.Itens.Sum(x => x.PrecoUnitario * x.Quantidade);
            Assert.Equal(orcamento.TotalItens, totalItens);
            
            var totalProdutos = command.Itens.Sum(x => x.Quantidade * x.PrecoUnitario) - totalDesconto;
            Assert.Equal(orcamento.TotalProdutos, totalProdutos);

            Assert.Equal(orcamento.TotalProdutos, exptected);
        }
        
    }
}