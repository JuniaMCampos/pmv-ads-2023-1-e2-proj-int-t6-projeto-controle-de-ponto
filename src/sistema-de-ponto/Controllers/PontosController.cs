using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Threading.Tasks;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;     
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sistema_de_ponto.Models;

namespace sistema_de_ponto.Controllers
{
    [Authorize]
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

        public async Task<IActionResult> Relatorio()
        {
           
            
            var applicationDbContext = _context.Pontos.Include(p => p.Funcionario).Include(e => e.Funcionario.Empresa);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Relatorio/ExportarPDF
        public IActionResult ExportarPDF()
        {
            var pontos = _context.Pontos
                .Include(e => e.Funcionario)
                .Include(e => e.Funcionario.Empresa)
                .ToList();

            var memoryStream = new MemoryStream();

            var writer = new PdfWriter(memoryStream);
            var pdfDocument = new PdfDocument(writer);
            var document = new Document(pdfDocument);

            PageSize pageSize = new PageSize(PageSize.A4.Rotate());
            pdfDocument.SetDefaultPageSize(pageSize);

            var titulo = new Paragraph("Relatório de Turnos por Funcionários");
            document.Add(titulo);

            

            // Cabeçalho
            var table = new Table(11).UseAllAvailableWidth();
            table.AddCell(new Cell().Add(new Paragraph(" ID ")));
            table.AddCell(new Cell().Add(new Paragraph(" Turno ")));
            table.AddCell(new Cell().Add(new Paragraph(" Colaborador ")));
            table.AddCell(new Cell().Add(new Paragraph(" Sobrenome ")));
            table.AddCell(new Cell().Add(new Paragraph(" Empresa ")));
            table.AddCell(new Cell().Add(new Paragraph(" 1 Entrada ")));
            table.AddCell(new Cell().Add(new Paragraph(" 1 Saída ")));
            table.AddCell(new Cell().Add(new Paragraph(" Intervalo ")));
            table.AddCell(new Cell().Add(new Paragraph(" 2 Entrada ")));
            table.AddCell(new Cell().Add(new Paragraph(" 2 Saída ")));
            table.AddCell(new Cell().Add(new Paragraph(" Total de Horas ")));

            // Dados
            foreach (var item in pontos)
            {
                
              
                    table.AddCell(new Cell().Add(new Paragraph(item.Id.ToString())));
                    table.AddCell(new Cell().Add(new Paragraph(item.Turno)));
                    table.AddCell(new Cell().Add(new Paragraph(item.Funcionario.Nome)));
                    table.AddCell(new Cell().Add(new Paragraph(item.Funcionario.Sobrenome)));
                    table.AddCell(new Cell().Add(new Paragraph(item.Funcionario.Empresa.Nome)));
                    table.AddCell(new Cell().Add(new Paragraph(item.HoraEntrada1.Value.ToShortTimeString())));
                    table.AddCell(new Cell().Add(new Paragraph(item.HoraSaida1.Value.ToShortTimeString())));
                    table.AddCell(new Cell().Add(new Paragraph(item.Intervalo.Value.ToString())));
                    table.AddCell(new Cell().Add(new Paragraph(item.HoraEntrada2.Value.ToShortTimeString())));
                    table.AddCell(new Cell().Add(new Paragraph(item.HoraSaida2.Value.ToShortTimeString())));
                    table.AddCell(new Cell().Add(new Paragraph(item.TotalDeHoras.Value.ToString())));



            }

            document.Add(table);

            // Rodapé do documento
            var dataHoraAtual = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time")); 
            var rodape = new Paragraph($"Apontei Sistemas Data: {dataHoraAtual.ToString("dd/MM/yyyy")}   Hora: {dataHoraAtual.ToString("HH:mm:ss")}");


            var numeroPaginas = pdfDocument.GetNumberOfPages();
            document.ShowTextAligned(rodape, 30, 30, numeroPaginas, TextAlignment.LEFT, VerticalAlignment.BOTTOM, 0);

            document.Close();

            var content = memoryStream.ToArray();
            return File(content, "application/pdf", "Relatorio_de_Turnos.pdf");






        }




        // GET: Pontos/Create
        public IActionResult Create()
        {
            ViewData["FuncionarioId"] = new SelectList(
                  _context.Funcionarios.Where(f => !f.Pontos.Any()), "Id", "Nome");

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


            ViewData["FuncionarioId"] = new SelectList(
                _context.Funcionarios.Where(f=> !f.Pontos.Any()), "Id", "Nome", ponto.FuncionarioId);

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
            
            
                try
                {
                    _context.Pontos.Remove(ponto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Não é possível excluir este turno, pois está sendo usado em outra aplicação.");

                }

                
            

            return RedirectToAction(nameof(Index));

        }

        private bool PontoExists(int id)
        {
            return _context.Pontos.Any(e => e.Id == id);
        }
    }
}
