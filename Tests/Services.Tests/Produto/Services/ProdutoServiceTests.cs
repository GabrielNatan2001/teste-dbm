using Application.UseCases.Produto;
using CommomTestsUtilities.Entities;
using CommomTestsUtilities.Mapper;
using CommomTestsUtilities.Repositories;
using CommomTestsUtilities.Requests;
using Communication.Requests;
using Exceptions.ExceptionsBase;
using FluentAssertions;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Services.Tests.Produto.Services
{
    public class ProdutoServiceTests
    {
        [Fact]
        public async Task Success_AddProduto()
        {
            var service = CreateService();
            var request = RequestProdutoJsonBuilder.Build();

            var result = await service.AddAsync(request);

            result.Should().NotBeNull();
            result.Nome.Should().Be(result.Nome);
        }
        

        [Fact]
        public async Task Success_GetAll()
        {
            var repository = new ProdutoRepositoryBuilder();
            var produtos = new List<Domain.Entities.Produto>
            {
                ProdutoBuilder.Build(),
                ProdutoBuilder.Build()
            };
            repository.WithGetAll(produtos);

            var service = CreateService(repository);

            var result = await service.GetAllAsync();

            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task Success_GetById()
        {
            var entity = ProdutoBuilder.Build();
            var repository = new ProdutoRepositoryBuilder();
            repository.WithGetById(entity.Id, entity);

            var service = CreateService(repository);

            var result = await service.GetByIdAsync(entity.Id);

            result.Should().NotBeNull();
        }

        [Fact]
        public async Task Success_Update()
        {
            var entity = ProdutoBuilder.Build();
            var repository = new ProdutoRepositoryBuilder();
            repository.WithGetById(entity.Id, entity);
            var request = RequestProdutoJsonBuilder.Build();
            var service = CreateService(repository);

            var result = await service.UpdateAsync(entity.Id, request);

            result.Should().NotBeNull();
        }

        [Fact]
        public async Task Success_Delete()
        {
            var entity = ProdutoBuilder.Build();
            var repository = new ProdutoRepositoryBuilder();
            repository.WithGetById(entity.Id, entity);

            var service = CreateService(repository);
            var result = await service.GetByIdAsync(entity.Id);

            result.Should().NotBeNull();
        }
        
        [Fact]
        public async Task Error_Delete_ProductNotFound()
        {
            var entity = ProdutoBuilder.Build();
            var service = CreateService();

            Func<Task> act = async () => await service.DeleteAsync(entity.Id);

           await act.Should().ThrowAsync<ProductNotFoundException>();
        }

        [Fact]
        public async Task Error_GetById_ProductNotFound()
        {
            var entity = ProdutoBuilder.Build();
            var service = CreateService();

            Func<Task> act = async () => await service.GetByIdAsync(entity.Id);

           await act.Should().ThrowAsync<ProductNotFoundException>();
        }

        [Fact]
        public async Task Error_Validation_Name_Empty()
        {
            var service = CreateService();

            var request = RequestProdutoJsonBuilder.Build();
            request.Nome = "";

            Func<Task> act = async () => await service.AddAsync(request);

           await act.Should().ThrowAsync<ErrorOnValidationException>()
                .Where(ex => ex.ErrorMessages.Contains("Nome é obrigatório"));
        }
        [Fact]
        public async Task Error_Validation_Name_More_100_caracters()
        {
            var service = CreateService();

            var request = RequestProdutoJsonBuilder.Build();
            request.Nome = new string('A', 101);

            Func<Task> act = async () => await service.AddAsync(request);

           var rsult = await act.Should().ThrowAsync<ErrorOnValidationException>()
                .Where(ex => ex.ErrorMessages.Contains("Nome deve ter no máximo 100 caracteres"));
        }

        [Fact]
        public async Task Error_Validation_Price_invalid()
        {
            var service = CreateService();

            var request = RequestProdutoJsonBuilder.Build();
            request.Preco = 0;

            Func<Task> act = async () => await service.AddAsync(request);

           await act.Should().ThrowAsync<ErrorOnValidationException>()
                .Where(ex => ex.ErrorMessages.Contains("Preço deve ser maior que zero"));
        }

        private static ProdutoService CreateService(ProdutoRepositoryBuilder reposiotyBuilder = null)
        {
            var repository = new ProdutoRepositoryBuilder();
            var mapper = MapperBuilder.Build();

            if (reposiotyBuilder != null)
                repository = reposiotyBuilder;


            var validation = new ProdutoValidation(repository.Build());

            return new ProdutoService(repository.Build(), validation, mapper);
        }
    }
}
