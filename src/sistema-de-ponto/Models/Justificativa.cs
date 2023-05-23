using Microsoft.AspNetCore.Http;
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
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o Motivo")]
        public string Motivo { get; set; }

        [Display(Name = "Anexar Documento")]
        public string AnexarDocumento { get; set; }

        [NotMapped]
        //[FileExtensions(Extensions = "jpg,jpeg,pdf")]
        public IFormFile Arquivo { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o Status")]
        [Display(Name = "Status da solicitação")]
        public Status Status { get; set; }

        public int FuncionarioId { get; set; }

        [ForeignKey("FuncionarioId")]
        public Funcionario Funcionario { get; set; }

        public int PontoId { get; set; }

        [ForeignKey("PontoId")]
        public Ponto Ponto { get; set; }

    }

    public enum Status
    {
        Aprovado,
        Pendente,
        Reprovado
    }
}
