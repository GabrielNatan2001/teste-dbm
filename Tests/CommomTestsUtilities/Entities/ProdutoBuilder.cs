using Bogus;

namespace CommomTestsUtilities.Entities
{
    public class ProdutoBuilder
    {
        public static Domain.Entities.Produto Build()
        {

            var produto = new Faker<Domain.Entities.Produto>()
                .RuleFor(r => r.Id, (f) => f.Random.Int())
                .RuleFor(r => r.Nome, (f) => f.Lorem.Sentence(1))
                .RuleFor(r => r.Descricao, (f, u) => f.Lorem.Sentence(3))
                .RuleFor(r => r.Preco, (f) => f.Random.Decimal(10, 500))
                .RuleFor(r => r.DataCadastro, () => DateTime.UtcNow);

            return produto;
        }
    }
}
