using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Services;
using Application.Interfaces;
using Domain.Entities;
using Xunit;
using Moq;

namespace Tests
{
    public class ClienteServiceTests
    {
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;
        private readonly ClienteService _clienteService;

        public ClienteServiceTests()
        {
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _clienteService = new ClienteService(_clienteRepositoryMock.Object);
        }

        [Fact]
        public async Task CriarAsync_DeveCriarClienteComSucesso()
        {
            var clienteDto = new ClienteDto
            {
                Nome = "Teste",
                Email = "teste@example.com",
                Cpf = "123.456.789-00",
                Rg = "12.345.678-9",
                Contatos = new List<ContatoDto>
                {
                    new ContatoDto { Tipo = "Celular", Ddd = 11, Telefone = "999999999" }
                },
                Enderecos = new List<EnderecoDto>
                {
                    new EnderecoDto
                    {
                        Tipo = "Preferencial",
                        Cep = "01234-567",
                        Logradouro = "Rua Teste",
                        Numero = 100,
                        Bairro = "Centro",
                        Complemento = "",
                        Cidade = "São Paulo",
                        Estado = "SP",
                        Referencia = "Próximo ao mercado"
                    }
                }
            };

            _clienteRepositoryMock.Setup(r => r.ListarAsync())
                                  .ReturnsAsync(new List<Cliente>());
            _clienteRepositoryMock.Setup(r => r.SalvarAsync(It.IsAny<Cliente>()))
                                  .Returns(Task.CompletedTask);

            var result = await _clienteService.CriarAsync(clienteDto);

            Assert.NotNull(result);
            Assert.Equal("Teste", result.Nome);
            Assert.Single(result.Contatos);
            Assert.Single(result.Enderecos);
        }

        [Fact]
        public async Task AtualizarAsync_ClienteNaoExiste_DeveLancarExcecao()
        {

            _clienteRepositoryMock.Setup(r => r.ObterPorIdAsync(It.IsAny<int>()))
                                  .ReturnsAsync((Cliente)null);

            var clienteDto = new ClienteDto
            {
                Nome = "Novo Nome",
                Email = "novo@example.com",
                Cpf = "123.456.789-00",
                Rg = "98.765.432-1",
                Contatos = new List<ContatoDto>(),
                Enderecos = new List<EnderecoDto>()
            };


            await Assert.ThrowsAsync<Exception>(() => _clienteService.AtualizarAsync(1, clienteDto));
        }
    
        [Fact]
        public async Task CriarAsync_TipoContatoInvalido_DeveLancarExcecao()
        {
            var clienteDto = new ClienteDto
            {
                Nome = "Teste",
                Email = "teste@example.com",
                Cpf = "123.456.789-00",
                Rg = "12.345.678-9",
                Contatos = new List<ContatoDto>
                {
                    new ContatoDto { Tipo = "Whatsapp", Ddd = 11, Telefone = "999999999" }
                }, 
                Enderecos = new List<EnderecoDto>()
            };

            _clienteRepositoryMock.Setup(r => r.ListarAsync()).ReturnsAsync(new List<Cliente>());

            await Assert.ThrowsAsync<ArgumentException>(() => _clienteService.CriarAsync(clienteDto));
        }
    }
}
