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
    [Route("api/v{version:apiVersion}/caixa")]
    [Authorize]
    public class CaixaController : ControllerBase
    {
        private readonly ICaixaRepository caixaRepository;
        private readonly CaixaWorkflow caixaWorkflow;
        public CaixaController(
            ICaixaRepository caixaRepository, 
            CaixaWorkflow caixaWorkflow)
        {
            this.caixaRepository = caixaRepository;
            this.caixaWorkflow = caixaWorkflow;
        }

        /// <summary>
        /// Consultar todos os grupos
        /// </summary>
        /// <returns></returns>
        [HttpGet]        
        public IActionResult Get([FromQuery]string pesquisa)
        {
            return Ok(caixaRepository.Pesquisar(pesquisa));
        }

        [HttpGet("totais-por-data")]        
        public IActionResult GetTotais([FromBody] FiltroCaixaCommand filtro)
        {             
            //DateTime dtInicial, DateTime dtFinal
            return Ok(caixaRepository.ConsultarTotaisCaixa(filtro.dtInicial, filtro.dtFinal));
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
            return Ok(caixaRepository.GetCaixaResultPorId(id));
        }

        /// <summary>
        /// Adicionar (insert) um grupo
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]            
        public IActionResult Adicionar([FromBody] CaixaCommand command)
        {
            //Pega o usuário do token
            command.UsuarioId = new Guid(this.HttpContext.GetUsuarioId());
            
            caixaWorkflow.Add(command);
            if (caixaWorkflow.IsValid())
            {
                return Ok();
            }
            return BadRequest(caixaWorkflow.GetErrors());
        }

        /// <summary>
        /// Atualizar (update) um grupo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut] 
        [Route("{id}")]           
        public IActionResult Atualizar([FromRoute] Guid id, [FromBody] CaixaCommand command)
        {
            //Pega o usuário do token
            command.UsuarioId = new Guid(this.HttpContext.GetUsuarioId());

            //
            caixaWorkflow.Update(id, command);
            if (caixaWorkflow.IsValid())
            {
                return Ok();
            }
            return BadRequest(caixaWorkflow.GetErrors());
        }

        /// <summary>
        /// Excluir um grupo por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]            
        public IActionResult Deletar([FromRoute] Guid id)
        {
            caixaWorkflow.Delete(id);
            if (caixaWorkflow.IsValid())
            {
                return Ok();
            }
            return BadRequest(caixaWorkflow.GetErrors());
        }


    }
}