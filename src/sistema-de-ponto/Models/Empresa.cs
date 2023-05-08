using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistema_de_ponto.Models
{
    [Table("Empresa")]
    public class Empresa
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Obrigatório informar o Cnpj")]
        [Display(Name = "CNPJ")]
        public string Cnpj { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o Nome")]
        public string Nome { get; set; }

        public ICollection<Funcionario> Funcionarios { get; set; }
    }
}
