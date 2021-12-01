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
    [Route("api/v{version:apiVersion}/produto")]
    [Authorize]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ProdutoWorkflow _produtoWorkflow;
        public ProdutoController(
            IProdutoRepository produtoRepository, 
            ProdutoWorkflow produtoWorkflow)
        {
            _produtoRepository = produtoRepository;
            _produtoWorkflow = produtoWorkflow;
        }

        /// <summary>
        /// Consultar todos os grupos
        /// </summary>
        /// <returns></returns>
        [HttpGet]        
        public IActionResult Get()
        {
            return Ok(_produtoRepository.RetornarTodos());
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
            return Ok(_produtoRepository.ConsultarPorId(id));
        }

        /// <summary>
        /// Adicionar (insert) um grupo
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]            
        public IActionResult Adicionar([FromBody] ProdutoCommand command)
        {
            _produtoWorkflow.Add(command);
            if (_produtoWorkflow.IsValid())
            {
                return Ok();
            }
            return BadRequest(_produtoWorkflow.GetErrors());
        }

        /// <summary>
        /// Atualizar (update) um grupo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut] 
        [Route("{id}")]           
        public IActionResult Atualizar([FromRoute] Guid id, [FromBody] ProdutoCommand command)
        {
            _produtoWorkflow.Update(id, command);
            if (_produtoWorkflow.IsValid())
            {
                return Ok();
            }
            return BadRequest(_produtoWorkflow.GetErrors());
        }

        /// <summary>
        /// Excluir um grupo por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]            
        public IActionResult Deletar([FromRoute] Guid id)
        {
            _produtoWorkflow.Delete(id);
            if (_produtoWorkflow.IsValid())
            {
                return Ok();
            }
            return BadRequest(_produtoWorkflow.GetErrors());
        }


    }
}