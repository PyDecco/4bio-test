using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Application.Mappers;
using Domain.Enums;

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
            {
                ValidarTipoContato(contatoDto.Tipo);
                var novoContatoId = ObterProximoIdContato(cliente);
                cliente.AdicionarContato(new Contato(novoContatoId, contatoDto.Tipo, contatoDto.Ddd, contatoDto.Telefone));
            }

            foreach (var enderecoDto in clienteDto.Enderecos)
            {
                ValidarTipoEndereco(enderecoDto.Tipo);
                var novoEnderecoId = ObterProximoIdEndereco(cliente);
                cliente.AdicionarEndereco(new Endereco(novoId, enderecoDto.Tipo, enderecoDto.Cep, enderecoDto.Logradouro,
                enderecoDto.Numero, enderecoDto.Bairro, enderecoDto.Complemento, enderecoDto.Cidade, enderecoDto.Estado, enderecoDto.Referencia));
            }
            
            await _clienteRepository.SalvarAsync(cliente);
            return ClienteMapper.ToDto(cliente);
        }

        public async Task<ClienteDto> AtualizarAsync(int id, ClienteDto clienteDto)
        {
            var cliente = await _clienteRepository.ObterPorIdAsync(id)
                ?? throw new Exception("Cliente não encontrado.");

            cliente = new Cliente(id, clienteDto.Nome, clienteDto.Email, clienteDto.Cpf, clienteDto.Rg);

            foreach (var contatoDto in clienteDto.Contatos)
            {
                ValidarTipoContato(contatoDto.Tipo);
                var novoContatoId = ObterProximoIdContato(cliente);
                cliente.AdicionarContato(new Contato(novoContatoId, contatoDto.Tipo, contatoDto.Ddd, contatoDto.Telefone));
            }

            foreach (var enderecoDto in clienteDto.Enderecos)
            {
                ValidarTipoEndereco(enderecoDto.Tipo);
                var novoEnderecoId = ObterProximoIdEndereco(cliente);
                cliente.AdicionarEndereco(new Endereco(novoEnderecoId, enderecoDto.Tipo, enderecoDto.Cep, enderecoDto.Logradouro,
                                                        enderecoDto.Numero, enderecoDto.Bairro, enderecoDto.Complemento, enderecoDto.Cidade, enderecoDto.Estado, enderecoDto.Referencia));
            }

            await _clienteRepository.AtualizarAsync(cliente);
            return ClienteMapper.ToDto(cliente);
        }

        public async Task RemoverAsync(int id)
        {
            await _clienteRepository.RemoverAsync(id);
        }

        private void ValidarTipoContato(string tipo)
        {
            if (!Enum.TryParse<TipoContato>(tipo, true, out _))
            {
                throw new ArgumentException("Tipo de contato inválido. Valores aceitos: Residencial, Comercial, Celular.");
            }
        }

        private void ValidarTipoEndereco(string tipo)
        {
            if (!Enum.TryParse<TipoEndereco>(tipo, true, out _))
            {
                throw new ArgumentException("Tipo de endereço inválido. Valores aceitos: Preferencial, Entrega, Cobranca.");
            }
        }
    
        private int ObterProximoIdContato(Cliente cliente)
        {
            List<Contato> contatos = cliente.Contatos;
            if (contatos.Count == 0) return 1;

            int maxId = contatos.Max(c => c.Id);
            return maxId + 1;
        }

        private int ObterProximoIdEndereco(Cliente cliente)
        {
            List<Endereco> enderecos = cliente.Enderecos;
            if (enderecos.Count == 0) return 1;

            int maxId = enderecos.Max(e => e.Id);
            return maxId + 1;
        }
    }
}
