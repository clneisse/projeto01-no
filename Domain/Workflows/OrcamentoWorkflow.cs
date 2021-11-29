using System;
using UStart.Domain.Commands;
using UStart.Domain.Contracts.Repositories;
using UStart.Domain.Entities;
using UStart.Domain.UoW;

namespace UStart.Domain.Workflows
{
    public class OrcamentoWorkflow : WorkflowBase
    {
        private readonly IOrcamentoRepository _orcamentoRepository;
        
        private readonly IProdutoRepository _produtoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrcamentoWorkflow(IOrcamentoRepository orcamentoRepository,
        IProdutoRepository produtoRepository, IUnitOfWork unitOfWork)
        {
            _orcamentoRepository = orcamentoRepository;
            _unitOfWork = unitOfWork;
            _produtoRepository = produtoRepository;
        }

        public void Add(OrcamentoCommand command)
        {

            if (command.UsuarioId.HasValue == false)
            {
                this.AddError("UsuarioId", "Não foi possível carregar o usuário, tente logar novamente.");
            }

            if (!this.IsValid())
            {
                return;
            }
            var orcamento = new Orcamento(command);
            _orcamentoRepository.Add(orcamento);
            _unitOfWork.Commit();

            return;
        }

        public void Update(Guid id, OrcamentoCommand command)
        {
            var orcamento = _orcamentoRepository.ConsultarPorId(id);

            if (!this.IsValid())
            {
                return;
            }


            if (orcamento != null)
            {
                orcamento.Update(command);
                _orcamentoRepository.Update(orcamento);
                _unitOfWork.Commit();
            }
            else
            {
                AddError("Orcamento", "Orçamento não encontrado", id);
            }
        }

        public void Delete(Guid id)
        {
            try
            {
                var orcamento = _orcamentoRepository.ConsultarPorId(id);
                if (orcamento == null)
                {
                    AddError("Orcamento", "Orçamento de dados não encontrado", id);
                }
                if (!IsValid())
                {
                    return;
                }

                _orcamentoRepository.Delete(orcamento);
                _unitOfWork.Commit();
            }
            catch (System.Exception exp)
            {
                if (this.isDevelopment())
                    AddException("Orcamento", exp);
                else throw;
            }
        }
    }
}