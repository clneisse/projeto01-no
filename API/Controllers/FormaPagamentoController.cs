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
    [Route("api/v{version:apiVersion}/forma-pagamento")]
    [Authorize]
    public class FormaPagamentoController : ControllerBase
    {
        private readonly IFormaPagamentoRepository _formaPagamentoRepository;
        private readonly FormaPagamentoWorkflow _formaPagamentoWorkflow;
        public FormaPagamentoController(
            IFormaPagamentoRepository formaPagamentoRepository, 
            FormaPagamentoWorkflow formaPagamentoWorkflow)
        {
            _formaPagamentoRepository = formaPagamentoRepository;
            _formaPagamentoWorkflow = formaPagamentoWorkflow;
        }

        /// <summary>
        /// Consultar todos os grupos
        /// </summary>
        /// <returns></returns>
        [HttpGet]        
        public IActionResult GetPesquisar([FromQuery]string pesquisa)
        {
            return Ok(_formaPagamentoRepository.Pesquisar(pesquisa));
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
            return Ok(_formaPagamentoRepository.ConsultarPorId(id));
        }

        /// <summary>
        /// Adicionar (insert) um grupo
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]            
        public IActionResult Adicionar([FromBody] FormaPagamentoCommand command)
        {
            _formaPagamentoWorkflow.Add(command);
            if (_formaPagamentoWorkflow.IsValid())
            {
                return Ok();
            }
            return BadRequest(_formaPagamentoWorkflow.GetErrors());
        }

        /// <summary>
        /// Atualizar (update) um grupo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut] 
        [Route("{id}")]           
        public IActionResult Atualizar([FromRoute] Guid id, [FromBody] FormaPagamentoCommand command)
        {
            _formaPagamentoWorkflow.Update(id, command);
            if (_formaPagamentoWorkflow.IsValid())
            {
                return Ok();
            }
            return BadRequest(_formaPagamentoWorkflow.GetErrors());
        }

        /// <summary>
        /// Excluir um grupo por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]            
        public IActionResult Deletar([FromRoute] Guid id)
        {
            _formaPagamentoWorkflow.Delete(id);
            if (_formaPagamentoWorkflow.IsValid())
            {
                return Ok();
            }
            return BadRequest(_formaPagamentoWorkflow.GetErrors());
        }


    }
}