using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UStart.Domain.Commands;
using UStart.Domain.Contracts.Repositories;
using UStart.Domain.Workflows;

namespace UStart.API.Controllers
{

    /// <summary>
    /// Exemplo de rota http://localhost:5000/api/v1/cliente/id
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/fornecedor")]
    [Authorize]
    public class FornecedorController : ControllerBase
    {
        private readonly IFornecedorRepository fornecedorRepository;
        private readonly FornecedorWorkflow FornecedorWorkflow;

        public FornecedorController(IFornecedorRepository fornecedorRepository, FornecedorWorkflow fornecedorWorkflow)
        {
            this.fornecedorRepository = fornecedorRepository;
            this.fornecedorWorkflow = fornecedorWorkflow;
        }
        
        [HttpGet]        
        public IActionResult Get([FromQuery] string pesquisa)
        {
            return Ok(fornecedorRepository.Pesquisar(pesquisa));
        }
        
        [HttpGet]    
        [Route("{id}")]    
        public IActionResult GetPorId([FromRoute] Guid id)
        {
            return Ok(fornecedorRepository.ConsultarPorId(id));
        }

        [HttpPost]
        public IActionResult Adicionar([FromBody] FornecedorCommand command )
        {
            fornecedorWorkflow.Add(command);
            if (fornecedorWorkflow.IsValid()){
                return Ok();
            }
            return BadRequest(fornecedorWorkflow.GetErrors());
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Atualizar([FromRoute] Guid id, [FromBody] FornecedorCommand command )
        {
            fornecedorWorkflow.Update(id, command);
            if (fornecedorWorkflow.IsValid()){
                return Ok();
            }
            return BadRequest(fornecedorWorkflow.GetErrors());
        }

        [HttpDelete("{id}")]        
        public IActionResult Excluir([FromRoute] Guid id)
        {
            fornecedorWorkflow.Delete(id);
            if (fornecedorWorkflow.IsValid()){
                return Ok();
            }
            return BadRequest(fornecedorWorkflow.GetErrors());
        }
    }
}
