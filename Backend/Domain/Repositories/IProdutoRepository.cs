using Domain.Entities;

namespace Domain.Repositories
{
    public interface IProdutoRepository
    {
        Task<Produto> GetByIdAsync(int id);
        Task<IEnumerable<Produto>> GetAllAsync();
        Task AddAsync(Produto produto);
        Task UpdateAsync(Produto produto);
        Task DeleteAsync(int id);
    }
}
