using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("cliente")]
    public class ClienteController(IClienteService clienteService) : ControllerBase
    {
        private readonly IClienteService _clienteService = clienteService;

        [HttpGet("listar")]
        public async Task<ActionResult<IEnumerable<ClienteDto>>> Listar([FromQuery] ClienteFiltroDto filtro)
        {
            var clientes = await _clienteService.ListarAsync(filtro);
            return Ok(clientes);
        }

        [HttpPost("criar")]
        public async Task<ActionResult<ClienteDto>> Criar([FromBody] ClienteDto clienteDto)
        {
            var clienteCriado = await _clienteService.CriarAsync(clienteDto);
            return CreatedAtAction(nameof(Listar), new { id = clienteCriado.Id }, clienteCriado);
        }

        [HttpPut("atualizar/{id}")]
        public async Task<ActionResult<ClienteDto>> Atualizar(int id, [FromBody] ClienteDto clienteDto)
        {
            var clienteAtualizado = await _clienteService.AtualizarAsync(id, clienteDto);
            return Ok(clienteAtualizado);
        }

        [HttpDelete("remover/{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            await _clienteService.RemoverAsync(id);
            return NoContent();
        }
    }
}
