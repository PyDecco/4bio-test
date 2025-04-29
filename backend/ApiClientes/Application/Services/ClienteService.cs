using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Application.Mappers;

namespace Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<IEnumerable<ClienteDto>> ListarAsync(string? nome = null, string? email = null, string? cpf = null)
        {
            var clientes = await _clienteRepository.ListarAsync();

            if (!string.IsNullOrWhiteSpace(nome))
                clientes = clientes.Where(c => c.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(email))
                clientes = clientes.Where(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(cpf))
                clientes = clientes.Where(c => c.Cpf == cpf);

            return clientes.Select(ClienteMapper.ToDto);
        }

        public async Task<ClienteDto> CriarAsync(ClienteDto clienteDto)
        {
            var clientes = await _clienteRepository.ListarAsync();
            var novoId = clientes.Any() ? clientes.Max(c => c.Id) + 1 : 1;

            var cliente = new Cliente(novoId, clienteDto.Nome, clienteDto.Email, clienteDto.Cpf, clienteDto.Rg);

            foreach (var contatoDto in clienteDto.Contatos)
                cliente.AdicionarContato(new Contato(contatoDto.Id, contatoDto.Tipo, contatoDto.Ddd, contatoDto.Telefone));

            foreach (var enderecoDto in clienteDto.Enderecos)
                cliente.AdicionarEndereco(new Endereco(enderecoDto.Id, enderecoDto.Tipo, enderecoDto.Cep, enderecoDto.Logradouro,
                                                        enderecoDto.Numero, enderecoDto.Bairro, enderecoDto.Complemento, enderecoDto.Cidade, enderecoDto.Estado, enderecoDto.Referencia));

            await _clienteRepository.SalvarAsync(cliente);
            return ClienteMapper.ToDto(cliente);
        }

        public async Task<ClienteDto> AtualizarAsync(int id, ClienteDto clienteDto)
        {
            var cliente = await _clienteRepository.ObterPorIdAsync(id)
                ?? throw new Exception("Cliente não encontrado.");

            cliente = new Cliente(id, clienteDto.Nome, clienteDto.Email, clienteDto.Cpf, clienteDto.Rg);

            foreach (var contatoDto in clienteDto.Contatos)
                cliente.AdicionarContato(new Contato(contatoDto.Id, contatoDto.Tipo, contatoDto.Ddd, contatoDto.Telefone));

            foreach (var enderecoDto in clienteDto.Enderecos)
                cliente.AdicionarEndereco(new Endereco(enderecoDto.Id, enderecoDto.Tipo, enderecoDto.Cep, enderecoDto.Logradouro,
                                                        enderecoDto.Numero, enderecoDto.Bairro, enderecoDto.Complemento, enderecoDto.Cidade, enderecoDto.Estado, enderecoDto.Referencia));

            await _clienteRepository.AtualizarAsync(cliente);
            return ClienteMapper.ToDto(cliente);
        }

        public async Task RemoverAsync(int id)
        {
            await _clienteRepository.RemoverAsync(id);
        }

        public async Task<ContatoDto> AdicionarContatoAsync(int idCliente, ContatoDto contatoDto)
        {
            var cliente = await _clienteRepository.ObterPorIdAsync(idCliente)
                ?? throw new Exception("Cliente não encontrado.");

            var novoContato = new Contato(contatoDto.Id, contatoDto.Tipo, contatoDto.Ddd, contatoDto.Telefone);
            cliente.AdicionarContato(novoContato);

            await _clienteRepository.AtualizarAsync(cliente);

            return contatoDto;
        }

        public async Task<ContatoDto> AtualizarContatoAsync(int idCliente, ContatoDto contatoDto)
        {
            var cliente = await _clienteRepository.ObterPorIdAsync(idCliente)
                ?? throw new Exception("Cliente não encontrado.");

            var contatoExistente = cliente.Contatos.FirstOrDefault(c => c.Id == contatoDto.Id)
                ?? throw new Exception("Contato não encontrado.");

            cliente.Contatos.Remove(contatoExistente);
            cliente.AdicionarContato(new Contato(contatoDto.Id, contatoDto.Tipo, contatoDto.Ddd, contatoDto.Telefone));

            await _clienteRepository.AtualizarAsync(cliente);

            return contatoDto;
        }

        public async Task RemoverContatoAsync(int idCliente, int idContato)
        {
            var cliente = await _clienteRepository.ObterPorIdAsync(idCliente)
                ?? throw new Exception("Cliente não encontrado.");

            var contato = cliente.Contatos.FirstOrDefault(c => c.Id == idContato)
                ?? throw new Exception("Contato não encontrado.");

            cliente.Contatos.Remove(contato);

            await _clienteRepository.AtualizarAsync(cliente);
        }

        public async Task<EnderecoDto> AdicionarEnderecoAsync(int idCliente, EnderecoDto enderecoDto)
        {
            var cliente = await _clienteRepository.ObterPorIdAsync(idCliente)
                ?? throw new Exception("Cliente não encontrado.");

            var novoEndereco = new Endereco(enderecoDto.Id, enderecoDto.Tipo, enderecoDto.Cep, enderecoDto.Logradouro,
                                            enderecoDto.Numero, enderecoDto.Bairro, enderecoDto.Complemento, enderecoDto.Cidade, enderecoDto.Estado, enderecoDto.Referencia);
            cliente.AdicionarEndereco(novoEndereco);

            await _clienteRepository.AtualizarAsync(cliente);

            return enderecoDto;
        }

        public async Task<EnderecoDto> AtualizarEnderecoAsync(int idCliente, EnderecoDto enderecoDto)
        {
            var cliente = await _clienteRepository.ObterPorIdAsync(idCliente)
                ?? throw new Exception("Cliente não encontrado.");

            var enderecoExistente = cliente.Enderecos.FirstOrDefault(e => e.Id == enderecoDto.Id)
                ?? throw new Exception("Endereço não encontrado.");

            cliente.Enderecos.Remove(enderecoExistente);
            cliente.AdicionarEndereco(new Endereco(enderecoDto.Id, enderecoDto.Tipo, enderecoDto.Cep, enderecoDto.Logradouro,
                                                    enderecoDto.Numero, enderecoDto.Bairro, enderecoDto.Complemento, enderecoDto.Cidade, enderecoDto.Estado, enderecoDto.Referencia));

            await _clienteRepository.AtualizarAsync(cliente);

            return enderecoDto;
        }

        public async Task RemoverEnderecoAsync(int idCliente, int idEndereco)
        {
            var cliente = await _clienteRepository.ObterPorIdAsync(idCliente)
                ?? throw new Exception("Cliente não encontrado.");

            var endereco = cliente.Enderecos.FirstOrDefault(e => e.Id == idEndereco)
                ?? throw new Exception("Endereço não encontrado.");

            cliente.Enderecos.Remove(endereco);

            await _clienteRepository.AtualizarAsync(cliente);
        }

    }
}
