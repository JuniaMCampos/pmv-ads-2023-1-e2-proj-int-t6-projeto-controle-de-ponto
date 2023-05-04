using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;

namespace Apontei.Models
{
    public class Justificativa
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A data é obrigatória.")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "O motivo é obrigatório.")]
        public string Motivo { get; set; }

        [Display(Name = "Documento")]
        public string AnexarDocumento { get; set; }

        [Required(ErrorMessage = "o status é obrigatória")]
        [Display(Name = "Status do Pedido")]
        public Status Status { get; set; }
    }
    public enum Status
    {
        Aprovado,
        Pendente,
        Reprovado
    }
}
