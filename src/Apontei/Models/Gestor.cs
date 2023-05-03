using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apontei.Models
{
    public class Gestor : Funcionario
    {
       [Key]
       public int Id { get; set; }

       public ICollection<Funcionario> Funcionarios { get; set; }

    }
}
