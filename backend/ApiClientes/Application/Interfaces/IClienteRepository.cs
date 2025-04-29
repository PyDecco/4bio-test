using Domain.Entities;

namespace Application.Interfaces
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> ListarAsync();
        Task<Cliente?> ObterPorIdAsync(int id);
        Task SalvarAsync(Cliente cliente);
        Task AtualizarAsync(Cliente cliente);
        Task RemoverAsync(int id);
    }
}