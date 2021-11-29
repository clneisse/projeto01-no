using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UStart.Domain.Commands;
using UStart.Domain.Contracts.Repositories;
using UStart.Domain.Workflows;

namespace UStart.API.Controllers
{
    /// <summary>
    /// Exemplo de controller
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/pedido")]
    [Authorize]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly PedidoWorkflow _pedidoWorkflow;
        public PedidoController(
            IPedidoRepository pedidoRepository, 
            PedidoWorkflow pedidoWorkflow)
        {
            _pedidoRepository = pedidoRepository;
            _pedidoWorkflow = pedidoWorkflow;
        }

        /// <summary>
        /// Consultar todos os grupos
        /// </summary>
        /// <returns></returns>
        [HttpGet]        
        public IActionResult Get()
        {
            return Ok(_pedidoRepository.RetornarTodos());
        }

        /// <summary>
        /// Consultar apenas um grupo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]    
        [Route("{id}")]    
        public IActionResult GetPorId([FromRoute] Guid id)
        {
            return Ok(_pedidoRepository.ConsultarPorId(id));
        }

        /// <summary>
        /// Adicionar (insert) um grupo
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]            
        public IActionResult Adicionar([FromBody] PedidoCommand command)
        {
            _pedidoWorkflow.Add(command);
            if (_pedidoWorkflow.IsValid())
            {
                return Ok();
            }
            return BadRequest(_pedidoWorkflow.GetErrors());
        }

        /// <summary>
        /// Atualizar (update) um grupo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut] 
        [Route("{id}")]           
        public IActionResult Atualizar([FromRoute] Guid id, [FromBody] PedidoCommand command)
        {
            _pedidoWorkflow.Update(id, command);
            if (_pedidoWorkflow.IsValid())
            {
                return Ok();
            }
            return BadRequest(_pedidoWorkflow.GetErrors());
        }

        /// <summary>
        /// Excluir um grupo por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]            
        public IActionResult Deletar([FromRoute] Guid id)
        {
            _pedidoWorkflow.Delete(id);
            if (_pedidoWorkflow.IsValid())
            {
                return Ok();
            }
            return BadRequest(_pedidoWorkflow.GetErrors());
        }


    }
}