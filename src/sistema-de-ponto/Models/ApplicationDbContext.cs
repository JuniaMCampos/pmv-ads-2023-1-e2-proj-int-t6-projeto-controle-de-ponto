using Microsoft.EntityFrameworkCore;

namespace sistema_de_ponto.Models
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { }
        public DbSet<Empresa> Empresas { get; set; }

        public DbSet<Funcionario> Funcionarios { get; set; }

        public DbSet<Ponto> Pontos { get; set; }

        public DbSet<Justificativa> Justificativas { get; set; }
    }
}
