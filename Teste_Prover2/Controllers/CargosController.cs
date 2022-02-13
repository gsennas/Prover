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
using Microsoft.AspNetCore.Authorization;

namespace Teste_Prover.Controllers
{
    [Authorize]
    public class CargosController : Controller
    {
        private readonly CargoService _service;
        public CargosController(CargoService service)
        {
            _service = service;
        }




        public IActionResult Index()
        {
            return View(_service.GetAll());
        }


        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargo = _service.GetID(id.Value);
            if (cargo == null)
            {
                return NotFound();
            }

            return View(cargo);
        }
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Descricao")] Cargo cargo)
        {
            if (ModelState.IsValid)
            {
                _service.Create(cargo);

                return RedirectToAction(nameof(Index));
            }
            return View(cargo);
        }

        // GET: Cargos/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargo = _service.GetID(id.Value);
            if (cargo == null)
            {
                return NotFound();
            }
            return View(cargo);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Descricao")] Cargo cargo)
        {
            if (id != cargo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _service.Edit(cargo);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_service.CargoExists(cargo.Id))
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
            return View(cargo);
        }

        // GET: Cargos/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargo = _service.GetID(id.Value);
            if (cargo == null)
            {
                return NotFound();
            }

            return View(cargo);
        }

        // POST: Cargos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public  IActionResult DeleteConfirmed(int id)
        {
            _service.DeleteConfirmed(id);
            return RedirectToAction(nameof(Index));
        }

        
    }
}
