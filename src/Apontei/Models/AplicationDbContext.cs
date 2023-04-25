using Microsoft.EntityFrameworkCore;

namespace Apontei.Models
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Funcionario> Funcionarios { get; set; }
    }
}
