using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teste_Prover2.Data;
using Teste_Prover2.Models;

namespace Teste_Prover2.Services
{
    public class CargoService
    {
        private readonly ApplicationDbContext _context;

        public CargoService(ApplicationDbContext context)
        {
            _context = context;
        }


        public ICollection<Cargo> GetAll()
        {
            return _context.Cargo.ToList();

        }
        public Cargo GetID(int id)
        {
            return _context.Cargo.FirstOrDefault(c => c.Id == id);

        }


        public void Create(Cargo cargo)
        {
            _context.Add(cargo);
            _context.SaveChanges();
        }

        public void Edit(Cargo cargo)
        {
            _context.Update(cargo);
            _context.SaveChanges();
        }
        public void DeleteConfirmed(int id)
        {
            var cargo = GetID(id);
            _context.Cargo.Remove(cargo);
            _context.SaveChanges();

        }
        public bool CargoExists(int id)
        {
            return _context.Cargo.Any(e => e.Id == id);
        }
    }


}
