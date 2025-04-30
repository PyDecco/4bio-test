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
        public async Task<IEnumerable<ClienteDto>> ListarAsync(ClienteFiltroDto filtro)
        {
            var clientes = await _clienteRepository.ListarAsync();

            var filtrados = FiltrarClientes(clientes, filtro);

            return filtrados.Select(ClienteMapper.ToDto);
        }

        public async Task<ClienteDto> CriarAsync(ClienteDto clienteDto)
        {
            var clientes = await _clienteRepository.ListarAsync();
            int novoId = ObterProximoIdCliente(clientes);

            var cliente = ConstruirCliente(clienteDto, novoId);

            await _clienteRepository.SalvarAsync(cliente);

            return ClienteMapper.ToDto(cliente);
        }

        public async Task<ClienteDto> AtualizarAsync(int id, ClienteDto clienteDto)
        {
            var clienteExistente = await _clienteRepository.ObterPorIdAsync(id)
                ?? throw new Exception("Cliente não encontrado.");

            var clienteAtualizado = ConstruirCliente(clienteDto, id);

            await _clienteRepository.AtualizarAsync(clienteAtualizado);

            return ClienteMapper.ToDto(clienteAtualizado);
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
    
        private int ObterProximoIdCliente(IEnumerable<Cliente> clientes)
        {
            List<Cliente> listaClientes = clientes.ToList();
            if (listaClientes.Count == 0) return 1;

            int maxId = listaClientes.Max(c => c.Id);
            return maxId + 1;
        }
    
        private IEnumerable<Cliente> FiltrarClientes(IEnumerable<Cliente> clientes, ClienteFiltroDto filtro)
        {
            if (!string.IsNullOrWhiteSpace(filtro.Nome))
                clientes = clientes.Where(c => c.Nome.Contains(filtro.Nome, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(filtro.Email))
                clientes = clientes.Where(c => c.Email.Equals(filtro.Email, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(filtro.Cpf))
                clientes = clientes.Where(c => c.Cpf == filtro.Cpf);

            return clientes;
        }
    
        private Cliente ConstruirCliente(ClienteDto dto, int novoId)
        {
            var cliente = new Cliente(novoId, dto.Nome, dto.Email, dto.Cpf, dto.Rg);

            int contatoId = 1;
            foreach (var contato in dto.Contatos)
            {
                ValidarTipoContato(contato.Tipo);
                cliente.AdicionarContato(new Contato(contatoId++, contato.Tipo, contato.Ddd, contato.Telefone));
            }

            int enderecoId = 1;
            foreach (var endereco in dto.Enderecos)
            {
                ValidarTipoEndereco(endereco.Tipo);
                cliente.AdicionarEndereco(new Endereco(enderecoId++, endereco.Tipo, endereco.Cep, endereco.Logradouro,
                    endereco.Numero, endereco.Bairro, endereco.Complemento, endereco.Cidade, endereco.Estado, endereco.Referencia));
            }

            return cliente;
        }
    }
}
