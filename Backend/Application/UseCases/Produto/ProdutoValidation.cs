using Communication.Requests;
using Domain.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Produto
{
    public class ProdutoValidation : AbstractValidator<RequestProdutoJson>
    {
        private readonly IProdutoRepository _produtoRepository;
        public ProdutoValidation(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;

            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório")
                .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres")
            .MustAsync(async (nome, cancellation) => await NomeUnico(nome))
            .WithMessage("Já existe um produto com este nome");

            RuleFor(p => p.Preco)
                .GreaterThan(0).WithMessage("Preço deve ser maior que zero")
                .NotNull().WithMessage("Preço é obrigatório");
        }
        private async Task<bool> NomeUnico(string nome)
        {
            var result = await _produtoRepository.NameExists(nome);
            return !result;
        }
    }
}
