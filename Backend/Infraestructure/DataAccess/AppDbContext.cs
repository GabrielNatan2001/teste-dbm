using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.DataAccess
{
    public class AppDbContext: DbContext
    {
        public DbSet<Produto> Produtos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {}
    }
}
