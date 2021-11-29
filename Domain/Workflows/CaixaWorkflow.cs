using System;
using UStart.Domain.Commands;
using UStart.Domain.Contracts.Repositories;
using UStart.Domain.Entities;
using UStart.Domain.UoW;

namespace UStart.Domain.Workflows
{
    public class CaixaWorkflow : WorkflowBase
    {
        private readonly ICaixaRepository _caixaRepository;
        
        private readonly IProdutoRepository _produtoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CaixaWorkflow(ICaixaRepository caixaRepository,
        IProdutoRepository produtoRepository, IUnitOfWork unitOfWork)
        {
            _caixaRepository = caixaRepository;
            _unitOfWork = unitOfWork;
            _produtoRepository = produtoRepository;
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
            _caixaRepository.Add(caixa);
            _unitOfWork.Commit();

            return;
        }

        public void Update(Guid id, CaixaCommand command)
        {
            var caixa = _caixaRepository.ConsultarPorId(id);

            if (!this.IsValid())
            {
                return;
            }


            if (caixa != null)
            {
                caixa.Update(command);
                _caixaRepository.Update(caixa);
                _unitOfWork.Commit();
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
                var caixa = _caixaRepository.ConsultarPorId(id);
                if (caixa == null)
                {
                    AddError("Registro de caixa", "Registro de caixa não encontrado", id);
                }
                if (!IsValid())
                {
                    return;
                }

                _caixaRepository.Delete(caixa);
                _unitOfWork.Commit();
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