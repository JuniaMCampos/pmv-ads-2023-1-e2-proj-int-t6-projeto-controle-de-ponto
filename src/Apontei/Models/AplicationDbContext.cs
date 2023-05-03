using Microsoft.EntityFrameworkCore;

namespace Apontei.Models
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Funcionario> Funcionarios { get; set; }

        public DbSet<Empresa> Empresas { get; set; }

        public DbSet<Gestor> Gestores { get; set; }

        public DbSet<Ponto> Pontos { get; set; }
    }
}
