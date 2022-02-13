using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Teste_Prover2.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName ="nvachar(100")]
        public string PrimeiroNome { get; set; }
        [PersonalData]
        [Column(TypeName = "nvachar(100")]
        public string UltimoNome { get; set; }
    }
}
