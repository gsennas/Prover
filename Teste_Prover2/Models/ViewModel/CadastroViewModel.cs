using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teste_Prover2.Models.ViewModel
{
    public class CadastroViewModel
    {
        public Cadastro Cadastro { get; set; }
        public ICollection<Cargo> Cargos { get; set; }
    }
}
