using Domain.Repositories;
using FluentMigrator.Runner;
using Infraestructure.DataAccess;
using Infraestructure.DataAccess.Repositories;
using Infraestructure.Extensions;
using Infraestructure.Migrations.Versions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure
{
    public static class DependecyInjectionExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbContext(services,configuration);

            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddPostgres()
                    .WithGlobalConnectionString(configuration.ConnectionString())
                    .ScanIn(typeof(Version000001).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole());

            AddRepositories(services);

            return services;
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.ConnectionString()));
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
        }
    }
}
