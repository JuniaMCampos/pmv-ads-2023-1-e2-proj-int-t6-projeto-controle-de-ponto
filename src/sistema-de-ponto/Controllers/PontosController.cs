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
    public class PontosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PontosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pontos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Pontos.Include(p => p.Funcionario);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Pontos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ponto = await _context.Pontos
                .Include(p => p.Funcionario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ponto == null)
            {
                return NotFound();
            }

            return View(ponto);
        }

        public async Task<IActionResult> Relatorio(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ponto = await _context.Pontos
                .Include(p => p.Funcionario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ponto == null)
            {
                return NotFound();
            }

            return View(ponto);
        }

        // GET: Pontos/Create
        public IActionResult Create()
        {
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Nome");
            return View();
        }

        // POST: Pontos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Turno,HoraEntrada1,HoraEntrada2,HoraSaida1,HoraSaida2, FuncionarioId")] Ponto ponto)
        {
            if (ModelState.IsValid)
            {
                ponto.TotalDeHoras = CalculateTotalHours(ponto);

                _context.Add(ponto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Nome", ponto.FuncionarioId);
            return View(ponto);
        }

        private TimeSpan? CalculateTotalHours(Ponto ponto)
        {
            if (ponto.HoraSaida2.HasValue)
            {
                var jornada1 = ponto.HoraSaida1 - ponto.HoraEntrada1;
                var jornada2 = ponto.HoraSaida2 - ponto.HoraEntrada2;
                return jornada1 + jornada2;
            }
            else if (ponto.HoraSaida1.HasValue)
            {
                return ponto.HoraSaida1 - ponto.HoraEntrada1;
            }
            else
            {
                return null;
            }
        }

        // GET: Pontos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ponto = await _context.Pontos.FindAsync(id);
            if (ponto == null)
            {
                return NotFound();
            }
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Nome", ponto.FuncionarioId);
            return View(ponto);
        }

        // POST: Pontos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Turno,HoraEntrada1,HoraEntrada2,HoraSaida1,HoraSaida2, FuncionarioId")] Ponto ponto)
        {
            if (id != ponto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ponto.TotalDeHoras = CalculateTotalHours(ponto);
                    _context.Update(ponto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PontoExists(ponto.Id))
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
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Nome", ponto.FuncionarioId);
            return View(ponto);
        }

        // GET: Pontos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ponto = await _context.Pontos
                .Include(p => p.Funcionario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ponto == null)
            {
                return NotFound();
            }

            return View(ponto);
        }

        // POST: Pontos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ponto = await _context.Pontos.FindAsync(id);
            _context.Pontos.Remove(ponto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PontoExists(int id)
        {
            return _context.Pontos.Any(e => e.Id == id);
        }
    }
}
