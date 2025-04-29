using Application.DTOs;
using Domain.Entities;

namespace Application.Mappers
{
    public static class ClienteMapper
    {
        public static ClienteDto ToDto(Cliente cliente)
        {
            return new ClienteDto
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Email = cliente.Email,
                Cpf = cliente.Cpf,
                Rg = cliente.Rg,
                Contatos = cliente.Contatos.Select(c => new ContatoDto
                {
                    Id = c.Id,
                    Tipo = c.Tipo,
                    Ddd = c.Ddd,
                    Telefone = c.Telefone
                }).ToList(),
                Enderecos = cliente.Enderecos.Select(e => new EnderecoDto
                {
                    Id = e.Id,
                    Tipo = e.Tipo,
                    Cep = e.Cep,
                    Logradouro = e.Logradouro,
                    Numero = e.Numero,
                    Bairro = e.Bairro,
                    Complemento = e.Complemento,
                    Cidade = e.Cidade,
                    Estado = e.Estado,
                    Referencia = e.Referencia
                }).ToList()
            };
        }

        public static Cliente ToDomain(ClienteDto dto)
        {
            var cliente = new Cliente(dto.Id, dto.Nome, dto.Email, dto.Cpf, dto.Rg);

            foreach (var contato in dto.Contatos)
                cliente.AdicionarContato(new Contato(contato.Id, contato.Tipo, contato.Ddd, contato.Telefone));

            foreach (var endereco in dto.Enderecos)
                cliente.AdicionarEndereco(new Endereco(endereco.Id, endereco.Tipo, endereco.Cep, endereco.Logradouro,
                                                        endereco.Numero, endereco.Bairro, endereco.Complemento, endereco.Cidade, endereco.Estado, endereco.Referencia));

            return cliente;
        }
    }
}
