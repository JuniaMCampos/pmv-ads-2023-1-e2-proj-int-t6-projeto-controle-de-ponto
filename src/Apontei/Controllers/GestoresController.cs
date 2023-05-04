using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Apontei.Models;

namespace Apontei.Controllers
{
    public class GestoresController : Controller
    {
        private readonly AplicationDbContext _context;

        public GestoresController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: Gestores
        public async Task<IActionResult> Index()
        {
            var aplicationDbContext = _context.Gestores.Include(g => g.Empresa).Include(g => g.Gestor);
            return View(await aplicationDbContext.ToListAsync());
        }

        // GET: Gestores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gestor = await _context.Gestores
                .Include(g => g.Empresa)
                .Include(g => g.Gestor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gestor == null)
            {
                return NotFound();
            }

            return View(gestor);
        }

        // GET: Gestores/Create
        public IActionResult Create()
        {
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "Id", "CNPJ");
            ViewData["GestorId"] = new SelectList(_context.Gestores, "Id", "Cargo");
            return View();
        }

        // POST: Gestores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Sobrenome,Cpf,Pis,Departamento,Cargo,Telefone,Email,Senha,Perfil,ImagemPerfil,EmpresaId,GestorId")] Gestor gestor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gestor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "Id", "CNPJ", gestor.EmpresaId);
            ViewData["GestorId"] = new SelectList(_context.Gestores, "Id", "Cargo", gestor.GestorId);
            return View(gestor);
        }

        // GET: Gestores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gestor = await _context.Gestores.FindAsync(id);
            if (gestor == null)
            {
                return NotFound();
            }
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "Id", "CNPJ", gestor.EmpresaId);
            ViewData["GestorId"] = new SelectList(_context.Gestores, "Id", "Cargo", gestor.GestorId);
            return View(gestor);
        }

        // POST: Gestores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Sobrenome,Cpf,Pis,Departamento,Cargo,Telefone,Email,Senha,Perfil,ImagemPerfil,EmpresaId,GestorId")] Gestor gestor)
        {
            if (id != gestor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gestor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GestorExists(gestor.Id))
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
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "Id", "CNPJ", gestor.EmpresaId);
            ViewData["GestorId"] = new SelectList(_context.Gestores, "Id", "Cargo", gestor.GestorId);
            return View(gestor);
        }

        // GET: Gestores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gestor = await _context.Gestores
                .Include(g => g.Empresa)
                .Include(g => g.Gestor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gestor == null)
            {
                return NotFound();
            }

            return View(gestor);
        }

        // POST: Gestores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gestor = await _context.Gestores.FindAsync(id);
            _context.Gestores.Remove(gestor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GestorExists(int id)
        {
            return _context.Gestores.Any(e => e.Id == id);
        }
    }
}
