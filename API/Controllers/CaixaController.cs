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
        private readonly ICaixaRepository _caixaRepository;
        private readonly CaixaWorkflow _caixaWorkflow;
        public CaixaController(
            ICaixaRepository caixaRepository, 
            CaixaWorkflow caixaWorkflow)
        {
            _caixaRepository = caixaRepository;
            _caixaWorkflow = caixaWorkflow;
        }

        /// <summary>
        /// Consultar todos os grupos
        /// </summary>
        /// <returns></returns>
        [HttpGet]        
        public IActionResult Get([FromQuery]string pesquisa)
        {
            return Ok(_caixaRepository.Pesquisar(pesquisa));
        }

        [HttpGet("totais-por-data")]        
        public IActionResult GetTotais([FromBody] FiltroCaixaCommand filtro)
        {             
            //DateTime dtInicial, DateTime dtFinal
            return Ok(_caixaRepository.ConsultarTotaisCaixa(filtro.dtInicial, filtro.dtFinal));
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
            return Ok(_caixaRepository.GetCaixaResultPorId(id));
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
            
            _caixaWorkflow.Add(command);
            if (_caixaWorkflow.IsValid())
            {
                return Ok();
            }
            return BadRequest(_caixaWorkflow.GetErrors());
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
            _caixaWorkflow.Update(id, command);
            if (_caixaWorkflow.IsValid())
            {
                return Ok();
            }
            return BadRequest(_caixaWorkflow.GetErrors());
        }

        /// <summary>
        /// Excluir um grupo por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]            
        public IActionResult Deletar([FromRoute] Guid id)
        {
            _caixaWorkflow.Delete(id);
            if (_caixaWorkflow.IsValid())
            {
                return Ok();
            }
            return BadRequest(_caixaWorkflow.GetErrors());
        }


    }
}