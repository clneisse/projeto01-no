using System;
using UStart.Domain.Commands;
using UStart.Domain.Contracts.Repositories;
using UStart.Domain.Entities;
using UStart.Domain.UoW;

namespace UStart.Domain.Workflows
{
    public class FornecedorWorkflow : WorkflowBase
    {
        private readonly IFornecedorRepository fornecedorRepository;
        private readonly IUnitOfWork _unitOfWork;
        public FornecedorWorkflow(IFornecedorRepository fornecedorRepository, IUnitOfWork unitOfWork)
        {
            this.fornecedorRepository = fornecedorRepository;
            _unitOfWork = unitOfWork;
        }

        public Fornecedor Add(FornecedorCommand command)
        {

            if (string.IsNullOrEmpty(command.Nome))
            {
                this.AddError("Nome", "Nome não informado");
            }

            if (this.IsValid() == false)
            {
                return null;
            }

            var fornecedor = new Fornecedor(command);
            fornecedorRepository.Add(fornecedor);
            _unitOfWork.Commit();

            return fornecedor;
        }

        public void Update(Guid id, FornecedorCommand command){
            
            var fornecedor = fornecedorRepository.ConsultarPorId(id);
            if (fornecedor != null){
                fornecedor.Update(command);
                fornecedorRepository.Update(fornecedor);
                _unitOfWork.Commit();
            }
            else{
                AddError("Fornecedor", "Fornecedor não pode ser encontrado", id);
            }

        }

        public void Delete(Guid id){

            //Cliente Cliente = _ClienteRepository.ConsultarPorId(id);

            var fornecedor = fornecedorRepository.ConsultarPorId(id);
            if (fornecedor != null){
                fornecedorRepository.Delete(fornecedor);
                _unitOfWork.Commit();                
            }else{
                AddError("Fornecedor", "Fornecedor não pode ser encontrado", id);
            }            
        }
    }
}