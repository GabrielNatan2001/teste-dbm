using Communication.Requests;
using Communication.Response;

namespace Application.UseCases.Produto
{
    public interface IProdutoService
    {
        Task<ResponseProdutoJson> GetByIdAsync(int id);
        Task<IEnumerable<ResponseProdutoJson>> GetAllAsync();
        Task<ResponseProdutoJson> AddAsync(RequestProdutoJson produto);
        Task<ResponseProdutoJson> UpdateAsync(int id,RequestProdutoJson produto);
        Task DeleteAsync(int id);
    }
}
