using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistema_de_ponto.Models
{
    [Table ("Ponto")]
    public class Ponto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Obritatório informar a Data")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "Obritatório informar a Hora de Entrada")]
        [Display(Name = "Hora de Entrada")]
        public TimeSpan HoraEntrada { get; set; }

        [Required(ErrorMessage = "Obritatório informar a Hora Intervalo Inicial")]
        [Display(Name = "Hora Intervalo Inicial")]
        public TimeSpan HoraIntervaloInicial { get; set; }

        [Required(ErrorMessage = "Obritatório informar a Hora Intervalo Final")]
        [Display(Name = "Hora Intervalo Final")]
        public TimeSpan HoraIntervaloFinal { get; set; }

        [Required(ErrorMessage = "Obritatório informar a Hora de Saída")]
        [Display(Name = "Hora de Saída")]
        public TimeSpan HoraSaida { get; set; }

        [Display(Name = "Horas Extras")]
        public TimeSpan HoraExtra { get; set; }

        [Required(ErrorMessage = "Obritatório informar o Turno")]
        public string Turno { get; set; }

        public int FuncionarioId { get; set; }

        [ForeignKey("FuncionarioId")]
        public Funcionario Funcionario { get; set; }

        public ICollection<Justificativa> Justificativas { get; set; }
    }
}
