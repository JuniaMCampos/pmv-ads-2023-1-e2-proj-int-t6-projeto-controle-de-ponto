using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Apontei.Models
{
    [Table("Funcionarios")]
    public class Funcionario
    {
                 
            [Key]
            public int Id { get; set; }

            [Required(ErrorMessage = "O nome é obrigatório.")]
            [Display(Name = "Nome")]
            [StringLength(20)]
            public string Nome { get; set; }


            [Required(ErrorMessage = "O Sobrenome é obrigatório.")]
            [Display(Name = "Sobrenome")]
            [StringLength(20)]
            public string Sobrenome { get; set; }


            [Required(ErrorMessage = "O CPF é obrigatório.")]
            [Display(Name = "CPF")]
            [StringLength(20)]
            public string Cpf { get; set; }

            [Required(ErrorMessage = "O Pis é obrigatório.")]
            [Display(Name = "Pis")]
            [StringLength(20)]
            public string Pis { get; set; }
            [Required(ErrorMessage = "O Departamento é obrigatório.")]
            [Display(Name = "Departamento")]
            [StringLength(20)]
            public string Departamento { get; set; }

            [Required(ErrorMessage = "o Cargo é obrigatório.")]
            [Display(Name = "Cargo")]
            public string Cargo { get; set; }


            [Display(Name = "Telefone")]
            [StringLength(11)]
            public string Telefone { get; set; }

            [Required(ErrorMessage = "O Email é obrigatório.")]
            [Display(Name = "E-mail")]
            [StringLength(50)]
            public string Email { get; set; }

            [Required(ErrorMessage = "A Senha é obrigatório.")]
            [Display(Name = "Senha")]
            public string Senha { get; set; }

            [Required(ErrorMessage = "O perfil é obrigatorio")]
            public Perfil Perfil { get; set; }

            [Display(Name = "Foto")]
            public string ImagemPerfil { get; set; }

            public int EmpresaId { get; set; }
            [ForeignKey("EmpresaId")]
            public Empresa Empresa { get; set; }

            public int GestorId { get; set; }
            [ForeignKey("GestorId")]
            public Gestor Gestor { get; set; }

            public ICollection<Ponto> Pontos { get; set; }

    }

        public enum Perfil
        {
            Gestor,
            Funcionario
        }



    }

