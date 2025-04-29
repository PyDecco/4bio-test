using Application.Interfaces;
using Domain.Entities;
using System.Text.Json;

namespace Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private const string FilePath = "clientes.json"; // Caminho relativo ao projeto API (pode mudar depois)

        public async Task<IEnumerable<Cliente>> ListarAsync()
        {
            if (!File.Exists(FilePath))
                return new List<Cliente>();

            var json = await File.ReadAllTextAsync(FilePath);
            var clientes = JsonSerializer.Deserialize<List<Cliente>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return clientes ?? new List<Cliente>();
        }

        public async Task<Cliente?> ObterPorIdAsync(int id)
        {
            var clientes = await ListarAsync();
            return clientes.FirstOrDefault(c => c.Id == id);
        }

        public async Task SalvarAsync(Cliente cliente)
        {
            var clientes = (await ListarAsync()).ToList();

            cliente = AtualizarIds(cliente, clientes);

            clientes.Add(cliente);
            await EscreverArquivoAsync(clientes);
        }

        public async Task AtualizarAsync(Cliente clienteAtualizado)
        {
            var clientes = (await ListarAsync()).ToList();

            var clienteExistente = clientes.FirstOrDefault(c => c.Id == clienteAtualizado.Id);
            if (clienteExistente != null)
            {
                clientes.Remove(clienteExistente);
                clientes.Add(clienteAtualizado);

                await EscreverArquivoAsync(clientes);
            }
        }

        public async Task RemoverAsync(int id)
        {
            var clientes = (await ListarAsync()).ToList();

            var clienteExistente = clientes.FirstOrDefault(c => c.Id == id);
            if (clienteExistente != null)
            {
                clientes.Remove(clienteExistente);
                await EscreverArquivoAsync(clientes);
            }
        }

        private async Task EscreverArquivoAsync(IEnumerable<Cliente> clientes)
        {
            var json = JsonSerializer.Serialize(clientes, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            await File.WriteAllTextAsync(FilePath, json);
        }

        private Cliente AtualizarIds(Cliente cliente, List<Cliente> clientes)
        {
            var novoIdCliente = clientes.Any() ? clientes.Max(c => c.Id) + 1 : 1;

            // Cria novo cliente já atualizado (assume que o domínio permite isso)
            var novoCliente = new Cliente(novoIdCliente, cliente.Nome, cliente.Email, cliente.Cpf, cliente.Rg);

            // Atualizar os IDs de Contatos
            var novoIdContato = 1;
            foreach (var contato in cliente.Contatos)
            {
                novoCliente.AdicionarContato(new Contato(novoIdContato++, contato.Tipo, contato.Ddd, contato.Telefone));
            }

            // Atualizar os IDs de Endereços
            var novoIdEndereco = 1;
            foreach (var endereco in cliente.Enderecos)
            {
                novoCliente.AdicionarEndereco(new Endereco(novoIdEndereco++, endereco.Tipo, endereco.Cep, endereco.Logradouro,
                    endereco.Numero, endereco.Bairro, endereco.Complemento, endereco.Cidade, endereco.Estado, endereco.Referencia));
            }

            return novoCliente;
        }
    }
}
