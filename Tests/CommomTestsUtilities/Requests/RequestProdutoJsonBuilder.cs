using Bogus;
using Bogus.DataSets;
using Communication.Requests;

namespace CommomTestsUtilities.Requests
{
    public static class RequestProdutoJsonBuilder
    {
        public static RequestProdutoJson Build()
        {
            var request = new Faker<RequestProdutoJson>()
                .RuleFor(r => r.Nome, (f) => f.Lorem.Sentence(1))
                .RuleFor(r => r.Descricao, (f, u) => f.Lorem.Sentence(3))
                .RuleFor(r => r.Preco, (f) => f.Random.Decimal(10, 500));

            return request;
        }
    }
}
