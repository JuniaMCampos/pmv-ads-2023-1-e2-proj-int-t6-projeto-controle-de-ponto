using System.ComponentModel.DataAnnotations;

namespace Apontei.Models
{
    public class Gestor : Funcionario
    {
       [Key]
       public int Id { get; set; }

    }
}
