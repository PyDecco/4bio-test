# Desafio Tecnico 4bio
<p align="center">
  <img src="site_logo.jpg" alt="4bio Logo" width="200"/>
</p>

# 🧾 API de Gerenciamento de Clientes

## 📘 Sobre o Projeto

Este projeto consiste em uma API REST desenvolvida em **.NET 8**, cujo objetivo é **gerenciar informações de clientes**, incluindo seus **contatos** e **endereços**.

A proposta foi desenvolvida como parte de um desafio técnico, com foco em:

- Aplicar **boas práticas de desenvolvimento backend**
- Utilizar os princípios **SOLID**
- Garantir **validações consistentes de entrada**
- Persistir dados em um arquivo **JSON** como base de dados

A API oferece endpoints completos para **criar, listar, atualizar e remover** clientes, respeitando formatos específicos para CPF, e-mail, RG, CEP, e tipos definidos para contatos e endereços.

---

## 📋 Requisitos Atendidos

- [x] Estrutura em camadas (Domain, Application, Infrastructure, API)
- [x] Uso de orientação a objetos e princípios SOLID
- [x] Injeção de dependência aplicada via DI container
- [x] Persistência de dados em arquivo JSON
- [x] Validação dos campos obrigatórios:
  - [x] E-mail válido
  - [x] CPF válido (formato e algoritmo)
  - [x] RG válido (formato e tamanho)
  - [x] CEP válido
- [x] Tipos permitidos para `Contato`:
  - Celular, Residencial, Comercial
- [x] Tipos permitidos para `Endereço`:
  - Preferencial, Entrega, Cobranca
- [x] Funcionalidades da API:
  - [x] Listar clientes com filtros opcionais (`nome`, `email`, `cpf`)
  - [x] Criar novo cliente
  - [x] Atualizar cliente existente (dados básicos, contatos e endereços)
  - [x] Remover cliente
  - [x] Remover contato/endereço de um cliente
- [x] Rotas implementadas:
  - [x] `GET /cliente/listar`
  - [x] `POST /cliente/criar`
  - [x] `PUT /cliente/atualizar/{id}`
  - [x] `DELETE /cliente/remover/{id}`

## 🏗️ Arquitetura

O projeto segue uma arquitetura em camadas baseada nos princípios da **Clean Architecture** e no **DDD (Domain-Driven Design)**, com foco em separação de responsabilidades, testabilidade e manutenibilidade.

A estrutura é composta pelas seguintes camadas:

### 🔹 `Domain`
Contém as **entidades de negócio** puras, sem dependência de infraestrutura ou bibliotecas externas. Essa camada define o **modelo central da aplicação**.

- Entidades: `Cliente`, `Contato`, `Endereco`
- Enums: `TipoContato`, `TipoEndereco`

### 🔹 `Application`
Responsável pelas **regras de negócio da aplicação**. Contém os **DTOs**, **interfaces de serviços**, **serviços de aplicação** e **mapeamentos** entre entidades e DTOs.

- Serviços: `ClienteService`
- DTOs: `ClienteDto`, `ContatoDto`, `EnderecoDto`
- Validações e lógica de domínio aplicada via DTO

### 🔹 `Infrastructure`
Contém a **implementação da persistência de dados** e infraestrutura auxiliar. No contexto deste projeto, persiste os dados em **arquivo JSON**.

- Repositório: `ClienteRepository` (implementa `IClienteRepository`)
- Armazenamento: `clientes.json` gerenciado via `System.IO`

### 🔹 `API`
Camada de apresentação (controller REST). Expõe os **endpoints HTTP**, recebe os DTOs via HTTP, chama os serviços da camada de aplicação e retorna os resultados.

- Controller: `ClienteController`
- Rotas: `GET`, `POST`, `PUT`, `DELETE` para clientes

---

Essa organização permite um fluxo claro de responsabilidades, facilita testes unitários e mantém o sistema modular e desacoplado.

## 🚀 Como rodar o projeto

Siga os passos abaixo para clonar, configurar e executar o projeto localmente:

### 1. Clone o repositório

```bash
git clone https://github.com/PyDecco/4bio-test.git
```

### 2. Acesse a pasta do projeto backend

```bash
cd 4bio-test/backend/ApiClientes
```

### 3. Compile o projeto

```bash
dotnet build
```

### 4. Execute a API localmente

```bash
dotnet run
```

A aplicação estará disponível em:

```bash
http://localhost:5131/swagger
```

### 5. Copie o arquivo de exemplo `clientes.json`

Após executar o projeto, ao utilizar qualquer endpoint, será gerado um arquivo `clientes.json` com array vazio dentro do projeto no diretorio:

```bash
bin/Debug/net8.0/clientes.json
```

copie o conteudo `clientes.json` que está na raiz do projeto, e cole-o nesse path ou simplismente rode o comando: 

```bash
cp ../../clientes.json bin/Debug/net8.0/clientes.json
```

## 🧪 Como rodar os testes

O projeto possui dois tipos de testes: **funcionais** (via Postman) e **unitários** (via xUnit + Moq).

---

### ✅ Testes Funcionais com Postman

1. Abra o Postman
2. Clique em **Importar**
3. Selecione o arquivo `4bio.postman_collection.json` localizado na **raiz do repositório**
4. Execute os endpoints disponíveis para:
   - Criar cliente
   - Listar com filtros
   - Atualizar cliente
   - Remover cliente, contato e endereço

---

### ✅ Testes Unitários

Os testes unitários estão implementados com `xUnit` no projeto:

```bash
/backend/tests/ApiClientes.Tests
cd backend/tests/ApiClientes.Tests
dotnet test
```

Testes atualmente implementados:

 - CriarAsync_DeveCriarClienteComSucesso()

 - AtualizarAsync_ClienteNaoExiste_DeveLancarExcecao()

 - CriarAsync_TipoContatoInvalido_DeveLancarExcecao()

## ⚙️ Tecnologias utilizadas

Este projeto foi desenvolvido com as seguintes tecnologias e bibliotecas:

### 🧱 Backend
- [.NET 8.0](https://dotnet.microsoft.com/en-us/)
- ASP.NET Core Web API
- C#

### 📚 Pacotes e Ferramentas
- `Swashbuckle.AspNetCore` – geração automática de documentação Swagger
- `Microsoft.AspNetCore.OpenApi` – suporte a OpenAPI via anotação
- `coverlet.collector` – cobertura de testes para `dotnet test`

### 🧪 Testes
- `xUnit` – framework de testes unitários
- `Moq` – biblioteca de mocking para testes isolados
- `Microsoft.NET.Test.Sdk` – execução dos testes com .NET CLI

### 📦 Persistência
- Armazenamento em arquivo `clientes.json` com manipulação via `System.IO`

## 💡 Melhorias futuras

Alguns pontos que poderiam ser evoluídos em versões futuras:

- [ ] Implementar testes unitários mais abrangentes (validações de RG, CPF, CEP, e-mail etc.)
- [ ] Implementar testes de integração com base no JSON real
- [ ] Criar interface web (ex: frontend em React) para consumo da API
- [ ] Adicionar autenticação e autorização para proteger os endpoints
- [ ] Internacionalização das mensagens de erro
- [ ] Uso de arquivos `.json` versionados (ou persistência em banco relacional)
- [ ] Deploy automatizado da API em ambiente cloud (Azure, Vercel, etc.)

---

## 👤 Autor / Contato

Desenvolvido por **André Urpia**.

- GitHub: [@PyDecco](https://github.com/PyDecco)
- LinkedIn: [linkedin.com/in/andre-urpía](https://www.linkedin.com/in/andreurpia/) *(ou seu link personalizado real)*
