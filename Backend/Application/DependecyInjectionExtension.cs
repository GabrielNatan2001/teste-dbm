using Application.Services.AutoMapper;
using Application.UseCases.Produto;
using Communication.Requests;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependecyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AddValidation(services);
            AddUseCases(services);
            AddAutoMapper(services);
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddScoped(opt =>
                new AutoMapper.MapperConfiguration(opt =>
                {
                    opt.AddProfile(new AutoMapping());
                }).CreateMapper()
            );
        }

        public static void AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<IProdutoService, ProdutoService>();
        }

        public static void AddValidation(this IServiceCollection services)
        {
            services.AddScoped<IValidator<RequestProdutoJson>, ProdutoValidation>();
        }
    }
}
