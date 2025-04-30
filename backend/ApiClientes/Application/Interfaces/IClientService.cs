using Application.DTOs;

namespace Application.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDto>> ListarAsync(ClienteFiltroDto filtro);
        Task<ClienteDto> CriarAsync(ClienteDto clienteDto);
        Task<ClienteDto> AtualizarAsync(int id, ClienteDto clienteDto);
        Task RemoverAsync(int id);
    }
}
