using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Teste_Prover2.Areas.Identity.Data;
using Teste_Prover2.Models;

namespace Teste_Prover2.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Teste_Prover2.Models.Cargo> Cargo { get; set; }
        public DbSet<Teste_Prover2.Models.Cadastro> Cadastro { get; set; }
    }
}
