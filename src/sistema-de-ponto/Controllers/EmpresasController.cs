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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sistema_de_ponto.Models;


namespace sistema_de_ponto.Controllers
{
    public class EmpresasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmpresasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Empresas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Empresas.ToListAsync());
        }

        // GET: Empresas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empresa == null)
            {
                return NotFound();
            }

            return View(empresa);
        }


        // GET: Empresas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Empresas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cnpj,Nome")] Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empresa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(empresa);
        }

        // GET: Empresas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa == null)
            {
                return NotFound();
            }
            return View(empresa);
        }

        // POST: Empresas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cnpj,Nome")] Empresa empresa)
        {
            if (id != empresa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empresa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpresaExists(empresa.Id))
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
            return View(empresa);
        }

        // GET: Empresas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empresa == null)
            {
                return NotFound();
            }

            return View(empresa);
        }

        // POST: Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);
            _context.Empresas.Remove(empresa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpresaExists(int id)
        {
            return _context.Empresas.Any(e => e.Id == id);
        }

        // GET: Empresas/Relatorio
        public async Task<IActionResult> Relatorio()
        {
            var empresas = await _context.Empresas.Include(e => e.Funcionarios).ToListAsync();
            return View(empresas);
        }

        // GET: Relatorio/ExportarPDF
        public IActionResult ExportarPDF()
        {
            var empresas = _context.Empresas.Include(e => e.Funcionarios).ToList();

            var memoryStream = new MemoryStream();

            var writer = new PdfWriter(memoryStream);
            var pdfDocument = new PdfDocument(writer);
            var document = new Document(pdfDocument);

            var titulo = new Paragraph("Relatório de Empresas e Funcionários");
            document.Add(titulo);

            // Cabeçalho
            var table = new Table(5).UseAllAvailableWidth();
            table.AddCell(new Cell().Add(new Paragraph(" ID ")));
            table.AddCell(new Cell().Add(new Paragraph(" CNPJ ")));
            table.AddCell(new Cell().Add(new Paragraph(" Razão Social ")));
            table.AddCell(new Cell().Add(new Paragraph(" Colaborador ")));
            table.AddCell(new Cell().Add(new Paragraph(" Sobrenome ")));

            // Dados
            foreach (var empresa in empresas)
            {
                foreach (var funcionario in empresa.Funcionarios)
                {
                    table.AddCell(new Cell().Add(new Paragraph(empresa.Id.ToString())));
                    table.AddCell(new Cell().Add(new Paragraph(empresa.Cnpj)));
                    table.AddCell(new Cell().Add(new Paragraph(empresa.Nome)));
                    table.AddCell(new Cell().Add(new Paragraph(funcionario.Nome)));
                    table.AddCell(new Cell().Add(new Paragraph(funcionario.Sobrenome)));

                    
                }
            }

            document.Add(table);

            // Rodapé do documento
            var dataHoraAtual = DateTime.Now;
            var rodape = new Paragraph($"Data: {dataHoraAtual.ToString("dd/MM/yyyy")}   Hora: {dataHoraAtual.ToString("HH:mm:ss")}");

            
            var numeroPaginas = pdfDocument.GetNumberOfPages();
            document.ShowTextAligned(rodape, 30, 30, numeroPaginas, TextAlignment.LEFT, VerticalAlignment.BOTTOM, 0);

            document.Close();

            var content = memoryStream.ToArray();
            return File(content, "application/pdf", "Relatorio_de_Empresas.pdf");






        }
    }
}
