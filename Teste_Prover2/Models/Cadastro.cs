using System;
using System.ComponentModel.DataAnnotations;
using Teste_Prover2.Models.Entity;

namespace Teste_Prover2.Models
{
    public class Cadastro
    {
        
        public int Id { get; set; }
        public string Nome { get; set; }
        [DataType(DataType.PhoneNumber)]
        public int Telefone { get; set; }
       
        
        [Display(Name = "Data de Nascimento"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        
        public DateTime Nascimento { get; set; }
        public int Idade { get; set; }
        public Sexo Sexo { get; set; }
        public bool Status { get; set; }
        public Cargo Cargo { get; set; }
        [Display(Name = "Cargo")]
        public int CargoId { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}
