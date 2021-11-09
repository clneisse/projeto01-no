using System;
using UStart.Domain.Commands;

namespace UStart.Domain.Entities
{
    public class Produto
    {
        public Guid Id { get; private set; }
        public Guid GrupoProdutoId { get; private set; }
        public Guid FornecedorId {get; private set; }
        public GrupoProduto GrupoProduto { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public decimal PrecoCusto { get; private set; }
        public decimal PrecoVenda { get; private set; }
        public string UrlImagem { get; private set; }
        public string CodigoExterno { get; private set; }

        public Produto()
        {

        }

        public Produto(ProdutoCommand command)
        {
            Id = command.Id == Guid.Empty ? Guid.NewGuid() : command.Id;
            AtualizarCampos(command);
        }

        public void Update(ProdutoCommand command)
        {
            AtualizarCampos(command);
        }

        private void AtualizarCampos(ProdutoCommand command)
        {
            Descricao = command.Descricao;
            GrupoProdutoId = command.GrupoProdutoId;
            Nome = command.Nome;            
            PrecoCusto = command.PrecoCusto;
            PrecoVenda = command.PrecoVenda;
            UrlImagem = command.UrlImagem;
            CodigoExterno = command.CodigoExterno;
        }
    }
}