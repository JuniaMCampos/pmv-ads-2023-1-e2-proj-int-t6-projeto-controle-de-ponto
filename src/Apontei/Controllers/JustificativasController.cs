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
    public class JustificativasController : Controller
    {
        private readonly AplicationDbContext _context;

        public JustificativasController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: Justificativas
        public async Task<IActionResult> Index()
        {
            var aplicationDbContext = _context.Justificativas.Include(j => j.Empresa);
            return View(await aplicationDbContext.ToListAsync());
        }

        // GET: Justificativas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var justificativa = await _context.Justificativas
                .Include(j => j.Empresa)
                
                .FirstOrDefaultAsync(m => m.Id == id);
            if (justificativa == null)
            {
                return NotFound();
            }

            return View(justificativa);
        }

        // GET: Justificativas/Create
        public IActionResult Create()
        {
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "Id", "CNPJ");
           
            return View();
        }

        // POST: Justificativas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Data,Motivo,AnexarDocumento,Status,EmpresaId")] Justificativa justificativa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(justificativa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "Id", "CNPJ", justificativa.EmpresaId);
            
            return View(justificativa);
        }

        // GET: Justificativas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var justificativa = await _context.Justificativas.FindAsync(id);
            if (justificativa == null)
            {
                return NotFound();
            }
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "Id", "CNPJ", justificativa.EmpresaId);
            
            return View(justificativa);
        }

        // POST: Justificativas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Data,Motivo,AnexarDocumento,Status,EmpresaId")] Justificativa justificativa)
        {
            if (id != justificativa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(justificativa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JustificativaExists(justificativa.Id))
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
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "Id", "CNPJ", justificativa.EmpresaId);
            
            return View(justificativa);
        }

        // GET: Justificativas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var justificativa = await _context.Justificativas
                .Include(j => j.Empresa)
                
                .FirstOrDefaultAsync(m => m.Id == id);
            if (justificativa == null)
            {
                return NotFound();
            }

            return View(justificativa);
        }

        // POST: Justificativas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var justificativa = await _context.Justificativas.FindAsync(id);
            _context.Justificativas.Remove(justificativa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JustificativaExists(int id)
        {
            return _context.Justificativas.Any(e => e.Id == id);
        }
    }
}
