using System;
using UStart.Domain.Commands;
using UStart.Domain.Contracts.Repositories;
using UStart.Domain.Entities;
using UStart.Domain.UoW;

namespace UStart.Domain.Workflows
{
    public class FormaPagamentoWorkflow : WorkflowBase
    {
        private readonly IFormaPagamentoRepository _formaPagamentoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FormaPagamentoWorkflow(IFormaPagamentoRepository formaPagamentoRepository, IUnitOfWork unitOfWork)
        {
            _formaPagamentoRepository = formaPagamentoRepository;
            _unitOfWork = unitOfWork;
        }

        public FormaPagamento Add(FormaPagamentoCommand command)
        {
            var formaPagamento = new FormaPagamento(command);
            _formaPagamentoRepository.Add(formaPagamento);
            _unitOfWork.Commit();

            return formaPagamento;
        }

        public void Update(Guid id, FormaPagamentoCommand command)
        {
            var formaPagamento = _formaPagamentoRepository.ConsultarPorId(id);
            if (formaPagamento != null)
            {
                formaPagamento.Update(command);
                _formaPagamentoRepository.Update(formaPagamento);
                _unitOfWork.Commit();
            }
            else
            {
                AddError("FormaPagamento", "Forma de pagamento não encontrado", id);
            }
        }

        public void Delete(Guid id)
        {
            try
            {

                var formaPagamento = _formaPagamentoRepository.ConsultarPorId(id);
                if (formaPagamento == null)
                {
                    AddError("FormaPagamento", "Forma de pagamento de dados não encontrado", id);
                }
                if (!IsValid())
                {
                    return;
                }

                _formaPagamentoRepository.Delete(formaPagamento);
                _unitOfWork.Commit();
            }
            catch (System.Exception exp)
            {
                if (this.isDevelopment())
                    AddException("FormaPagamento", exp);
                else throw;
            }
        }
    }
}