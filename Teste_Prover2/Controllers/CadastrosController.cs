using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Teste_Prover2.Data;
using Teste_Prover2.Services;
using Teste_Prover2.Models;
using Teste_Prover2.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace Teste_Prover2.Controllers

{
    [Authorize]
    public class CadastrosController : Controller
    {
        private readonly CadastroService _service;
        private readonly CargoService _serviceCargo;

        public CadastrosController(CadastroService service, CargoService serviceCargo)
        {
            _service = service;
            _serviceCargo = serviceCargo;
        }


        public IActionResult Index()
        {
            return View(_service.GetAll());
        }
        public IActionResult Index2()
        {
            
                return View(_service.GetAtivos());
            
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cadastro = _service.GetID(id.Value);
            if (cadastro == null)
            {
                return NotFound();
            }

            return View(cadastro);
        }

        // GET: Cadastros/Create
        public IActionResult Create()
        {
            var cargo = _serviceCargo.GetAll();
            var view = new CadastroViewModel { Cargos = cargo };
            return View(view);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Nome,Telefone,Nascimento,Idade,Sexo,Status,CargoId")] Cadastro cadastro)
        {
            
                _service.Create(cadastro);
                
                return RedirectToAction(nameof(Index));
           
        }

       
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cadastro = _service.GetID(id.Value);
            if (cadastro == null)
            {
                return NotFound();
            }
            ViewData["CargoId"] = _service.SelectCargoId(cadastro.CargoId);
            return View(cadastro);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Nome,Telefone,Nascimento,Idade,Sexo,Status,CargoId")] Cadastro cadastro)
        {
            if (id != cadastro.Id)
            {
                return NotFound();
            }

            
                try
                {
                    _service.Edit(cadastro);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_service.CargoExists(cadastro.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cadastro = _service.GetID(id.Value);
            if (cadastro == null)
            {
                return NotFound();
            }

            return View(cadastro);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            
            _service.DeleteConfirmed(id);
            return RedirectToAction(nameof(Index));
        }
               
    }
}
