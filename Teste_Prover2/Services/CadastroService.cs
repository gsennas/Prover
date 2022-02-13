using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teste_Prover2.Models;
using Teste_Prover2.Models.ViewModel;
using Teste_Prover2.Data;

namespace Teste_Prover2.Services
{
    public class CadastroService
    {
        private readonly ApplicationDbContext _context;

        public CadastroService(ApplicationDbContext context)
        {
            _context = context;
        }


        public List<Cadastro> GetAll()
        {
            var teste_ProverContext = _context.Cadastro.Include(c => c.Cargo);

            return teste_ProverContext.ToList();

        }
        public List<Cadastro> GetAtivos()
        {
            var teste_ProverContext = _context.Cadastro.Include(c => c.Cargo).Where(s => s.Status == true);

            return teste_ProverContext.ToList();

        }

        public Cadastro GetID(int id)
        {
            return _context.Cadastro
                .Include(c => c.Cargo)
                .FirstOrDefault(m => m.Id == id);
        }


        public void Create(Cadastro cadastro)
        {
            var model = new Cadastro()
            { 
                Nome = cadastro.Nome,
                Sexo = cadastro.Sexo,
                Status = true,
                Telefone = cadastro.Telefone,
                Nascimento = cadastro.Nascimento,
                Idade = CalculaIdade(cadastro.Nascimento),
                CargoId = cadastro.CargoId,
                Cargo = cadastro.Cargo
            };

            _context.Add(model);
            _context.SaveChanges();
        }

        public void Edit(Cadastro cadastro)
        {
            var model = new Cadastro()
            {
                Id = cadastro.Id,
                Nome = cadastro.Nome,
                Sexo = cadastro.Sexo,
                Status = cadastro.Status,
                Telefone = cadastro.Telefone,
                Nascimento = cadastro.Nascimento,
                Idade = CalculaIdade(cadastro.Nascimento),
                CargoId = cadastro.CargoId,
                Cargo = cadastro.Cargo
            };
            _context.Update(model);
            _context.SaveChanges();
        }



        public void DeleteConfirmed(int id)
        {
            var cadastro = GetID(id);
            _context.Cadastro.Remove(cadastro);
            _context.SaveChanges();

        }
        public bool CargoExists(int id)
        {
            return _context.Cadastro.Any(e => e.Id == id);
        }
        public SelectList SelectCargo()
        {
            return new SelectList(_context.Set<Cargo>(), "Id", "Id");

        }
        public SelectList SelectCargoId(int id)
        {
            return new SelectList(_context.Set<Cargo>(), "Id", "Id", id);

        }
        private int CalculaIdade(DateTime datetime)
        {
            int idade = DateTime.Now.Year - datetime.Year;
            if (DateTime.Now.DayOfYear < datetime.DayOfYear)
            {
                return idade - 1;
            }
            return idade;
        }

    }
}
