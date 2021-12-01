using System;
using UStart.Domain.Commands;
using UStart.Domain.Contracts.Repositories;
using UStart.Domain.Entities;
using UStart.Domain.UoW;

namespace UStart.Domain.Workflows
{
    public class CaixaWorkflow : WorkflowBase
    {
        private readonly ICaixaRepository caixaRepository;
        
        private readonly IProdutoRepository produtoRepository;
        private readonly IUnitOfWork unitOfWork;

        public CaixaWorkflow(ICaixaRepository caixaRepository,
        IProdutoRepository produtoRepository, IUnitOfWork unitOfWork)
        {
            this.caixaRepository = caixaRepository;
            this.unitOfWork = unitOfWork;
            this.produtoRepository = produtoRepository;
        }

        public void Add(CaixaCommand command)
        {

            if (command.UsuarioId.HasValue == false)
            {
                this.AddError("UsuarioId", "Não foi possível carregar o usuário, tente logar novamente.");
            }

            if (!this.IsValid())
            {
                return;
            }
            var caixa = new Caixa(command);
            caixaRepository.Add(caixa);
            unitOfWork.Commit();

            return;
        }

        public void Update(Guid id, CaixaCommand command)
        {
            var caixa = caixaRepository.ConsultarPorId(id);

            if (!this.IsValid())
            {
                return;
            }


            if (caixa != null)
            {
                caixa.Update(command);
                caixaRepository.Update(caixa);
                unitOfWork.Commit();
            }
            else
            {
                AddError("Registro de caixa", "Registro de caixa não encontrado", id);
            }
        }

        public void Delete(Guid id)
        {
            try
            {
                var caixa = caixaRepository.ConsultarPorId(id);
                if (caixa == null)
                {
                    AddError("Registro de caixa", "Registro de caixa não encontrado", id);
                }
                if (!IsValid())
                {
                    return;
                }

                caixaRepository.Delete(caixa);
                unitOfWork.Commit();
            }
            catch (System.Exception exp)
            {
                if (this.isDevelopment())
                    AddException("Caixa", exp);
                else throw;
            }
        }
    }
}