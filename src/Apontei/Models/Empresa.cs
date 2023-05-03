using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apontei.Models
{
    public class Empresa
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Obrigatório Informar o CNPJ")]
        public string CNPJ { get; set; }

        [Required(ErrorMessage = "Obrigatório informar Nome")]
        public string Nome { get; set; }

    }
}
