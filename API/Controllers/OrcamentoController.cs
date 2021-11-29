using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UStart.API.TokenHelper;
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
    [Route("api/v{version:apiVersion}/orcamento")]
    [Authorize]
    public class OrcamentoController : ControllerBase
    {
        private readonly IOrcamentoRepository _orcamentoRepository;
        private readonly OrcamentoWorkflow _orcamentoWorkflow;
        public OrcamentoController(
            IOrcamentoRepository orcamentoRepository, 
            OrcamentoWorkflow orcamentoWorkflow)
        {
            _orcamentoRepository = orcamentoRepository;
            _orcamentoWorkflow = orcamentoWorkflow;
        }

        /// <summary>
        /// Consultar todos os grupos
        /// </summary>
        /// <returns></returns>
        [HttpGet]        
        public IActionResult Get([FromQuery]string pesquisa)
        {
            return Ok(_orcamentoRepository.Pesquisar(pesquisa));
        }

        [HttpGet("totais-por-data")]        
        public IActionResult GetTotais([FromBody] FiltroOrcamentoCommand filtro)
        {             
            //DateTime dtInicial, DateTime dtFinal
            return Ok(_orcamentoRepository.ConsultarTotaisOrcamento(filtro.dtInicial, filtro.dtFinal));
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
            return Ok(_orcamentoRepository.GetOrcamentoResultPorId(id));
        }

        /// <summary>
        /// Adicionar (insert) um grupo
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]            
        public IActionResult Adicionar([FromBody] OrcamentoCommand command)
        {
            //Pega o usuário do token
            command.UsuarioId = new Guid(this.HttpContext.GetUsuarioId());
            
            _orcamentoWorkflow.Add(command);
            if (_orcamentoWorkflow.IsValid())
            {
                return Ok();
            }
            return BadRequest(_orcamentoWorkflow.GetErrors());
        }

        /// <summary>
        /// Atualizar (update) um grupo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut] 
        [Route("{id}")]           
        public IActionResult Atualizar([FromRoute] Guid id, [FromBody] OrcamentoCommand command)
        {
            //Pega o usuário do token
            command.UsuarioId = new Guid(this.HttpContext.GetUsuarioId());

            //
            _orcamentoWorkflow.Update(id, command);
            if (_orcamentoWorkflow.IsValid())
            {
                return Ok();
            }
            return BadRequest(_orcamentoWorkflow.GetErrors());
        }

        /// <summary>
        /// Excluir um grupo por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]            
        public IActionResult Deletar([FromRoute] Guid id)
        {
            _orcamentoWorkflow.Delete(id);
            if (_orcamentoWorkflow.IsValid())
            {
                return Ok();
            }
            return BadRequest(_orcamentoWorkflow.GetErrors());
        }


    }
}