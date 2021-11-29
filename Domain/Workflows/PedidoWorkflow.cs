using System;
using UStart.Domain.Commands;
using UStart.Domain.Contracts.Repositories;
using UStart.Domain.Entities;
using UStart.Domain.UoW;

namespace UStart.Domain.Workflows
{
    public class PedidoWorkflow : WorkflowBase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PedidoWorkflow(IPedidoRepository pedidoRepository, IUnitOfWork unitOfWork)
        {
            _pedidoRepository = pedidoRepository;
            _unitOfWork = unitOfWork;
        }

        public Pedido Add(PedidoCommand command)
        {
            var pedido = new Pedido(command);
            _pedidoRepository.Add(pedido);
            _unitOfWork.Commit();

            return pedido;
        }

        public void Update(Guid id, PedidoCommand command)
        {
            var pedido = _pedidoRepository.ConsultarPorId(id);
            if (pedido != null)
            {
                pedido.Update(command);
                _pedidoRepository.Update(pedido);
                _unitOfWork.Commit();
            }
            else
            {
                AddError("Pedido", "Pedido não encontrado", id);
            }
        }

        public void Delete(Guid id)
        {
            try
            {

                var pedido = _pedidoRepository.ConsultarPorId(id);
                if (pedido == null)
                {
                    AddError("Pedido", "Pedido de dados não encontrado", id);
                }
                if (!IsValid())
                {
                    return;
                }

                _pedidoRepository.Delete(pedido);
                _unitOfWork.Commit();
            }
            catch (System.Exception exp)
            {
                if (this.isDevelopment())
                    AddException("Pedido", exp);
                else throw;
            }
        }
    }
}