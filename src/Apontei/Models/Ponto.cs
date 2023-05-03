using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apontei.Models
{
    public class Ponto
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Obrigatório Informar a Data")]
        public DateTime Data { get; set; }


        [Display(Name = "Hora de Entrada")]
        [Required(ErrorMessage = "Obrigatório Informar a Hora de Entrada")]
        public TimeSpan HoraEntrada { get; set; }


        [Required(ErrorMessage = "Obrigatório Informar a Hora do inicio do intervalo")]
        [Display(Name = "Hora Inicio Intervalo")]
        public TimeSpan HoraIntervaloInicial { get; set; }


        [Required(ErrorMessage = "Obrigatório Informar a Hora do fim do intervalo")]
        [Display(Name = "Hora Fim do Intervalo")]
        public TimeSpan HoraIntervaloFinal { get; set; }


        [Required(ErrorMessage = "Obrigatório Informar a Hora de Saída")]
        [Display(Name = "Hora de Saída")]
        public TimeSpan HoraSaida { get; set; }

        public TimeSpan HorasExtras { get; set; }
        [Display(Name = "Hora Extras")]


        [Required(ErrorMessage = "Obrigatório Informar o Turno")]
        public string Turno { get; set; }

        public int FuncionarioId { get; set; }
        [ForeignKey("FuncionarioId")]
        public Funcionario Funcionario { get; set; }
    }
}
