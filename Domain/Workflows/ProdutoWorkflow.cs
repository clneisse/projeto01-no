using System;
using UStart.Domain.Commands;
using UStart.Domain.Contracts.Repositories;
using UStart.Domain.Entities;
using UStart.Domain.UoW;

namespace UStart.Domain.Workflows
{
    public class ProdutoWorkflow : WorkflowBase
    {
        private readonly IProdutoRepository produtoRepository;
        private readonly IUnitOfWork unitOfWork;

        public ProdutoWorkflow(IProdutoRepository produtoRepository, IUnitOfWork unitOfWork)
        {
            this.produtoRepository = produtoRepository;
            this.unitOfWork = unitOfWork;
        }

        public Produto Add(ProdutoCommand command)
        {
            var Produto = new Produto(command);
            this.produtoRepository.Add(Produto);
            this.unitOfWork.Commit();

            return Produto;
        }

        public void Update(Guid id, ProdutoCommand command){

            if (ValidarProduto(command) == false){
                return;
            }
            
            var Produto = produtoRepository.ConsultarPorId(id);
            if (Produto != null){
                Produto.Update(command);
                produtoRepository.Update(Produto);
                unitOfWork.Commit();
            }
            else{
                AddError("Produto", "Produto não pode ser encontrado", id);
            }

        }
        public void Delete(Guid id)
        {
            try
            {

                var Produto = produtoRepository.ConsultarPorId(id);
                if (Produto == null)
                {
                    AddError("Produto", "Produto de dados não encontrado", id);
                }
                if (!IsValid())
                {
                    return;
                }

                produtoRepository.Delete(Produto);
                unitOfWork.Commit();
            }
            catch (System.Exception exp)
            {
                if (this.isDevelopment())
                    AddException("Produto", exp);
                else throw;
            }

        }
        private bool ValidarProduto(ProdutoCommand command){
            if (string.IsNullOrEmpty(command.Nome))
            {
                this.AddError("Nome", "Nome não informado");
            }
            if (command.GrupoProdutoId == Guid.Empty)
            {
                this.AddError("Grupo Produto", "Grupo produto não informado");
            }

            return this.IsValid();
        }
    }
}