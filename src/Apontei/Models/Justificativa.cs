using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

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

        public int FuncionarioId { get; set; }
        [ForeignKey("FuncionarioId")]
        public Funcionario Funcionario { get; set; }

        public int PontoId { get; set; }
        [ForeignKey("PontoId")]
        public Ponto Ponto{ get; set; }


      
    }
    public enum Status
    {
        Aprovado,
        Pendente,
        Reprovado
    }
}
