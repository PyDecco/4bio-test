using Application.DTOs;

namespace Application.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDto>> ListarAsync(string? nome = null, string? email = null, string? cpf = null);
        Task<ClienteDto> CriarAsync(ClienteDto clienteDto);
        Task<ClienteDto> AtualizarAsync(int id, ClienteDto clienteDto);
        Task RemoverAsync(int id);
    }
}
