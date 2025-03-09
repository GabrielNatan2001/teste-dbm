using Communication.Requests;
using Domain.Entities;
using Domain.Repositories;
using Moq;

namespace CommomTestsUtilities.Repositories
{
    public class ProdutoRepositoryBuilder
    {
        private readonly Mock<IProdutoRepository> _repository;
        public ProdutoRepositoryBuilder()
        {
            _repository = new Mock<IProdutoRepository>();
        }

        public ProdutoRepositoryBuilder WithGetById(int id, Produto produto)
        {
            _repository.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(produto);
            return this;
        }

        public ProdutoRepositoryBuilder WithGetAll(IEnumerable<Produto> produtos)
        {
            _repository.Setup(r => r.GetAllAsync()).ReturnsAsync(produtos);
            return this;
        }
        public ProdutoRepositoryBuilder NameExists(RequestProdutoJson produto)
        {
            _repository.Setup(r => r.NameExists(produto.Nome)).ReturnsAsync(true);
            return this;
        }

        public IProdutoRepository Build()
        {
            return _repository.Object;
        }
    }
}
