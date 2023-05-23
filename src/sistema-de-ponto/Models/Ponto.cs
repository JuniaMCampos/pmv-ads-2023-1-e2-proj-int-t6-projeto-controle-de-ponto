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

        [Required(ErrorMessage ="Horário de 1 Entrada obrigatório!")]
        [Display(Name = "1 Entrada")]
        [DisplayFormat(DataFormatString = "{0:HH\\:mm}")]
        public DateTime? HoraEntrada1 { get; set; }

        [Required(ErrorMessage = "Horário de 1 Saída obrigatório!")]
        [Display(Name = "1 Saída")]
        [DisplayFormat(DataFormatString = "{0:HH\\:mm}")]
        public DateTime? HoraSaida1 { get; set; }

        [Required(ErrorMessage = "Horário de 2 Entrada obrigatório!")]
        [Display(Name = "2 Entrada")]
        [DisplayFormat(DataFormatString = "{0:HH\\:mm}")]
        public DateTime? HoraEntrada2 { get; set; }

        [Required(ErrorMessage = "Horário de 2 Saída obrigatório!")]
        [Display(Name = "2 Saída")]
        [DisplayFormat(DataFormatString = "{0:HH\\:mm}")]
        public DateTime? HoraSaida2 { get; set; }

        [NotMapped]
        [Display(Name = "Intervalo")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        public TimeSpan? Intervalo
        {
            get
            {
                if (HoraSaida1.HasValue)
                {
                    return (TimeSpan)(HoraSaida1 - HoraEntrada2);
                }
                else
                {
                    return TimeSpan.Zero;
                }

            }
        }

        [Display(Name ="Total de Horas")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        public TimeSpan? TotalDeHoras { get; set; }
        



        [Required(ErrorMessage = "Obritatório informar o Nome do Turno")]
        public string Turno { get; set; }

        public int FuncionarioId { get; set; }
        
       
        [ForeignKey("FuncionarioId")]
        [Display(Name = "Nome do Colaborador")]
        public Funcionario Funcionario { get; set; }

        public ICollection<Justificativa> Justificativas { get; set; }
    }
}
