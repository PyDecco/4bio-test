using Application.DTOs;

namespace Application.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDto>> ListarAsync(string? nome = null, string? email = null, string? cpf = null);
        Task<ClienteDto> CriarAsync(ClienteDto clienteDto);
        Task<ClienteDto> AtualizarAsync(int id, ClienteDto clienteDto);
        Task RemoverAsync(int id);

        Task<ContatoDto> AdicionarContatoAsync(int idCliente, ContatoDto contatoDto);
        Task<ContatoDto> AtualizarContatoAsync(int idCliente, ContatoDto contatoDto);
        Task RemoverContatoAsync(int idCliente, int idContato);

        Task<EnderecoDto> AdicionarEnderecoAsync(int idCliente, EnderecoDto enderecoDto);
        Task<EnderecoDto> AtualizarEnderecoAsync(int idCliente, EnderecoDto enderecoDto);
        Task RemoverEnderecoAsync(int idCliente, int idEndereco);
    }
}
