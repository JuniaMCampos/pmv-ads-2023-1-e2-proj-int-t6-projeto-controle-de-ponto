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

        [NotMapped]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        public TimeSpan? TotalDeHoras
        {
            get
            {
                if (HoraSaida2.HasValue)
                {

                  var jornada1 = (TimeSpan)(HoraSaida1 - HoraEntrada1);
                  var jornada2 = (TimeSpan)(HoraSaida2 - HoraEntrada2);

                    return jornada1 + jornada2;


                }
                else if (HoraSaida1.HasValue)
                {
                    return (TimeSpan)(HoraSaida1.Value - HoraEntrada1);
                }
                else
                {
                    return TimeSpan.Zero;
                }
            }
        }



        [Required(ErrorMessage = "Obritatório informar o Nome do Turno")]
        public string Turno { get; set; }

        public int FuncionarioId { get; set; }

        [ForeignKey("FuncionarioId")]
        public Funcionario Funcionario { get; set; }

        public ICollection<Justificativa> Justificativas { get; set; }
    }
}
