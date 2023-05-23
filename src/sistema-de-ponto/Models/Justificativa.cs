using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistema_de_ponto.Models
{
    [Table("Justificativa")]
    public class Justificativa
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Obrigatório informar a Data")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o Motivo")]
        public string Motivo { get; set; }

        [Display(Name = "Anexar Documento")]
        public string AnexarDocumento { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o Status")]
        [Display(Name = "Status da solicitação")]
        public Status Status { get; set; }

        public int FuncionarioId { get; set; }

        [ForeignKey("FuncionarioId")]
        [Display(Name ="Colaborador")]
        public Funcionario Funcionario { get; set; }

        public int PontoId { get; set; }

        [ForeignKey("PontoId")]
        [Display(Name ="Turno")]
        public Ponto Ponto { get; set; }

    }

    public enum Status
    {
        Aprovado,
        Pendente,
        Reprovado
    }
}
