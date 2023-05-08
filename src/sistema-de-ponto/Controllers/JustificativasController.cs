using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sistema_de_ponto.Models;

namespace sistema_de_ponto.Controllers
{
    public class JustificativasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JustificativasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Justificativas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Justificativas.Include(j => j.Funcionario).Include(j => j.Ponto);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Justificativas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var justificativa = await _context.Justificativas
                .Include(j => j.Funcionario)
                .Include(j => j.Ponto)
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
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Cargo");
            ViewData["PontoId"] = new SelectList(_context.Pontos, "Id", "Turno");
            return View();
        }

        // POST: Justificativas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Data,Motivo,AnexarDocumento,Status,FuncionarioId,PontoId")] Justificativa justificativa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(justificativa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Cargo", justificativa.FuncionarioId);
            ViewData["PontoId"] = new SelectList(_context.Pontos, "Id", "Turno", justificativa.PontoId);
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
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Cargo", justificativa.FuncionarioId);
            ViewData["PontoId"] = new SelectList(_context.Pontos, "Id", "Turno", justificativa.PontoId);
            return View(justificativa);
        }

        // POST: Justificativas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Data,Motivo,AnexarDocumento,Status,FuncionarioId,PontoId")] Justificativa justificativa)
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
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Cargo", justificativa.FuncionarioId);
            ViewData["PontoId"] = new SelectList(_context.Pontos, "Id", "Turno", justificativa.PontoId);
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
                .Include(j => j.Funcionario)
                .Include(j => j.Ponto)
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
