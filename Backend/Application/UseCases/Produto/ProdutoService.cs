using AutoMapper;
using Communication.Requests;
using Communication.Response;
using Domain.Repositories;
using Exceptions.ExceptionsBase;
using FluentValidation;

namespace Application.UseCases.Produto
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IValidator<RequestProdutoJson> _validator;
        private readonly IMapper _mapper;
        public ProdutoService(IProdutoRepository produtoRepository, 
            IValidator<RequestProdutoJson> validator,
              IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<ResponseProdutoJson> GetByIdAsync(int id)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);
            if (produto == null)
                throw new ProductNotFoundException("Produto não encontrado.");
            return _mapper.Map<ResponseProdutoJson>(produto);
        }

        public async Task<IEnumerable<ResponseProdutoJson>> GetAllAsync()
        {
            var produtos = await _produtoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ResponseProdutoJson>>(produtos);
        }

        public async Task<ResponseProdutoJson> AddAsync(RequestProdutoJson request)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
                throw new ErrorOnValidationException(validationResult.Errors.Select(x=> x.ErrorMessage).ToList());

            var produto = _mapper.Map<Domain.Entities.Produto>(request);

            await _produtoRepository.AddAsync(produto);
            return _mapper.Map<ResponseProdutoJson>(produto);
        }

        public async Task<ResponseProdutoJson> UpdateAsync(int id, RequestProdutoJson request)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
                throw new ErrorOnValidationException(validationResult.Errors.Select(x => x.ErrorMessage).ToList());

            var produto = await _produtoRepository.GetByIdAsync(id);

            produto.Nome = request.Nome;
            produto.Descricao = request.Descricao;
            produto.Preco = request.Preco;

            await _produtoRepository.UpdateAsync(produto);
            return _mapper.Map<ResponseProdutoJson>(produto);
        }

        public async Task DeleteAsync(int id)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);
            if (produto == null)
                throw new ProductNotFoundException("Produto não encontrado.");

            await _produtoRepository.DeleteAsync(id);
        }
    }
}
