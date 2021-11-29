using UStart.Domain.Commands;
using UStart.Domain.Entities;
using Xunit;
using System.Linq;

namespace UStart.Tests
{
    public class Caixa_Test
    {
        public Caixa_Test()
        {
            
        }

        [Fact(DisplayName = "Teste criar novo caixa")]        
        public void Novo_Caixa_Test()
        {
            CaixaCommand command = new CaixaCommand();
            command.Itens.Add(new CaixaItemCommand(){
                Quantidade = 2,
                Desconto = 10,
                PrecoUnitario = 10            
            });
                
            Caixa caixa = new Caixa(command);

            Assert.Equal(caixa.Itens.Count, command.Itens.Count);
            Assert.Equal(caixa.QuantidadeDeItens, command.Itens.Count);            
        }

        [Theory(DisplayName = "Calcular totais e desconto")]
        [InlineData(2,10,10, 18)]
        [InlineData(10,10,10, 90)]
        [InlineData(10,0,10, 100)]
        [InlineData(0,10,10, 0)]
        public void Caixa_calcular_totais_Test(decimal quantidade, decimal desconto, decimal precoUni, decimal exptected)
        {
            CaixaCommand command = new CaixaCommand();
            command.Itens.Add(new CaixaItemCommand(){
                Quantidade = quantidade,
                Desconto = desconto,
                PrecoUnitario = precoUni           
            });

            var totalDesconto = command.Itens.Sum(x => x.Quantidade * x.PrecoUnitario * (x.Desconto /100));
                    
            Caixa caixa = new Caixa(command);

            Assert.Equal(caixa.Itens.Count, command.Itens.Count);
            Assert.Equal(caixa.QuantidadeDeItens, command.Itens.Count);
            Assert.Equal(caixa.TotalDesconto, totalDesconto);  

            var totalItens = command.Itens.Sum(x => x.PrecoUnitario * x.Quantidade);
            Assert.Equal(caixa.TotalItens, totalItens);
            
            var totalProdutos = command.Itens.Sum(x => x.Quantidade * x.PrecoUnitario) - totalDesconto;
            Assert.Equal(caixa.TotalProdutos, totalProdutos);

            Assert.Equal(caixa.TotalProdutos, exptected);
        }
        
    }
}