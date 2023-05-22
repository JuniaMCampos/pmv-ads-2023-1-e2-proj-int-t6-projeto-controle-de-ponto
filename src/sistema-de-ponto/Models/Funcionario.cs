using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistema_de_ponto.Models
{
    [Table("Funcionario")]
    public class Funcionario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Obrigatório informar o Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o Sobrenome")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o Cpf")]
        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o Pis")]
        [Display(Name = "PIS")]
        public string Pis { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o Departamento")]
        public string Departamento { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o Cargo")]
        public string Cargo { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o Telefone")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o Email")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Obrigatório informar a Senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o Perfil")]
        public Perfil Perfil { get; set; }

        //Esta caralha será salvo no banco
        public string Foto { get; set; }

        //Isso vou usar para facilitar o upload,
        //coloquei o NotMapped para não ir para o banco, só pra view, viu?
        [NotMapped]
        public IFormFile Arquivo { get; set; }

        public int? EmpresaId { get; set; }

        [ForeignKey("EmpresaId")]
        public Empresa Empresa { get; set; }

        public ICollection<Ponto> Pontos { get; set; }

        public ICollection<Justificativa> Justificativas { get; set; }

        public ICollection<RegistraPonto> RegistraPontos { get; set; }
    }

    public enum Perfil
    {
        Gestor,
        Funcionario
    }
}
