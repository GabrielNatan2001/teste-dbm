using CommomTestsUtilities.Entities;
using FluentAssertions;
using Infraestructure.DataAccess;
using Infraestructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Services.Tests.Produto.Repositories
{
    public class ProdutoRepositoryTests
    {
        private readonly AppDbContext _context;
        private readonly ProdutoRepository _repository;

        public ProdutoRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDatabase_{Guid.NewGuid()}")
                .Options;
            _context = new AppDbContext(options);
            _repository = new ProdutoRepository(_context);
        }

        [Fact]
        public async Task AddAsync_AddsProdutoToDatabase()
        {
           var produto = ProdutoBuilder.Build();

            await _repository.AddAsync(produto);
            await _context.SaveChangesAsync();

            var result = await _context.Produtos.FirstOrDefaultAsync(p => p.Nome == produto.Nome);
            result.Should().NotBeNull();
            result.Nome.Should().Be(produto.Nome);
            result.Descricao.Should().Be(produto.Descricao);
            result.Preco.Should().Be(produto.Preco);
            result.DataCadastro.Should().BeCloseTo(produto.DataCadastro, TimeSpan.FromSeconds(1));
        }

        [Fact]
        public async Task GetByIdAsync_ExistingId_ReturnsProduto()
        {

            var produto = ProdutoBuilder.Build();
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            var result = await _repository.GetByIdAsync(produto.Id);

            result.Should().NotBeNull();
            result.Id.Should().Be(produto.Id);
            result.Nome.Should().Be(produto.Nome);
        }

        [Fact]
        public async Task GetByIdAsync_NonExistingId_ReturnsNull()
        {
            var result = await _repository.GetByIdAsync(999);

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllProdutos()
        {
            var produtos = new List<Domain.Entities.Produto>
            {
                ProdutoBuilder.Build(),
                ProdutoBuilder.Build()
            };
            _context.Produtos.AddRange(produtos);
            await _context.SaveChangesAsync();

            var result = await _repository.GetAllAsync();

            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetAllAsync_EmptyDatabase_ReturnsEmptyList()
        {
            var result = await _repository.GetAllAsync();

            result.Should().BeEmpty();
        }
    }
}
