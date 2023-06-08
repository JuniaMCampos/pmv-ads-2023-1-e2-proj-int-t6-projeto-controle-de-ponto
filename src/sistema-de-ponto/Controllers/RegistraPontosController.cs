using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sistema_de_ponto.Models;



namespace sistema_de_ponto.Controllers
{
    [Authorize]
    public class RegistraPontosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegistraPontosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RegistraPontos
        public async Task<IActionResult> Index()
        {
            // Obtém o ID do funcionário associado ao usuário logado
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            

            var applicationDbContext = _context.RegistraPontos
                .Include(r => r.Funcionario)
                .Where(rp => rp.Funcionario.Nome == userId);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RegistraPontos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registraPonto = await _context.RegistraPontos
                .Include(r => r.Funcionario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registraPonto == null)
            {
                return NotFound();
            }

            return View(registraPonto);
        }

        // GET: RegistraPontos/Create
        public IActionResult Create()
        {
            var funcionarioId = ObterFuncionarioIdDoUsuarioLogado();
            if (funcionarioId == null)
            {
                // Caso o funcionário não esteja autenticado ou não esteja associado a um usuário, pode-se retornar uma mensagem de erro ou redirecionar para uma página de erro
                return RedirectToAction("Index", "RegistraPontos");
            }

            ViewData["FuncionarioId"] = funcionarioId;
            return View();
            
        }
        private int? ObterFuncionarioIdDoUsuarioLogado()
        {
            var usuarioLogado = _context.Funcionarios.FirstOrDefault(u => u.Nome == User.Identity.Name);
            return usuarioLogado?.Id;
        }

        // POST: RegistraPontos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FuncionarioId,Data,HoraEntrada1,HoraSaida1,HoraEntrada2,HoraSaida2")] RegistraPonto registraPonto)
        {
            var funcionarioId = ObterFuncionarioIdDoUsuarioLogado();
            // Obtém o último registro de ponto do funcionário
            var ultimoRegistroPonto = _context.RegistraPontos
                .Where(rp => rp.FuncionarioId == funcionarioId)
                .OrderByDescending(rp => rp.Data)
                .FirstOrDefault();
            // Cria um novo registro de ponto com a data e hora atuais
            DateTime horaAtualBrasilia = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));

            var novoRegistroPonto = new RegistraPonto
                {
                    Data = horaAtualBrasilia,
                    FuncionarioId = (int)funcionarioId,

                };
               

                if (ultimoRegistroPonto == null  || ultimoRegistroPonto.HoraSaida2 != null && ultimoRegistroPonto.Data != novoRegistroPonto.Data)
                {
                _context.Add(novoRegistroPonto);
                // Se não houver registros anteriores, define a hora de entrada 1
                novoRegistroPonto.HoraEntrada1 = horaAtualBrasilia;
                }
                else if (ultimoRegistroPonto.HoraSaida1 == null)
                {
                    // Se a hora de saída 1 não estiver definida, define a hora de saída 1
                    ultimoRegistroPonto.HoraSaida1 = horaAtualBrasilia;
                }
                else if (ultimoRegistroPonto.HoraEntrada2 == null)
                {
                    // Se a hora de entrada 2 não estiver definida, define a hora de entrada 2
                    ultimoRegistroPonto.HoraEntrada2 = horaAtualBrasilia;
                }
                else if (ultimoRegistroPonto.HoraSaida2 == null)
                {
                    // Se a hora de saída 2 não estiver definida, define a hora de saída 2
                    ultimoRegistroPonto.HoraSaida2 = horaAtualBrasilia;
                }
                else
                {
                    // Todas as etapas já foram registradas, pode-se adicionar uma lógica adicional ou retornar uma mensagem de erro se necessário

                    return RedirectToAction("Index", "RegistraPontos");
                }
 

                if (ModelState.IsValid)
                {
                    
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
         
            ViewData["FuncionarioId"] = funcionarioId;
            return View(registraPonto);
        }

        

        // GET: RegistraPontos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registraPonto = await _context.RegistraPontos.FindAsync(id);
            if (registraPonto == null)
            {
                return NotFound();
            }
            await _context.Entry(registraPonto)
                           .Reference(r => r.Funcionario)                         
                           .LoadAsync();

            ViewData["FuncionarioId"] = new SelectList(new List<Funcionario> {registraPonto.Funcionario }, "Id", "Nome", registraPonto.FuncionarioId);

            ViewBag.DataRegistro = registraPonto.Data.ToShortDateString();
            return View(registraPonto);
        }

        // POST: RegistraPontos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FuncionarioId,Data,HoraEntrada1,HoraSaida1,HoraEntrada2,HoraSaida2")] RegistraPonto registraPonto)
        {
            if (id != registraPonto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registraPonto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistraPontoExists(registraPonto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ControlePonto));
            }
            await _context.Entry(registraPonto)
                           .Reference(r => r.Funcionario)
                           .LoadAsync();
            new SelectList(new List<Funcionario> { registraPonto.Funcionario }, "Id", "Nome", registraPonto.FuncionarioId);
            
            return View(registraPonto);
        }

        // GET: RegistraPontos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registraPonto = await _context.RegistraPontos
                .Include(r => r.Funcionario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registraPonto == null)
            {
                return NotFound();
            }

            return View(registraPonto);
        }

        // POST: RegistraPontos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registraPonto = await _context.RegistraPontos.FindAsync(id);
            _context.RegistraPontos.Remove(registraPonto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ControlePonto));
        }

        private bool RegistraPontoExists(int id)
        {
            return _context.RegistraPontos.Any(e => e.Id == id);
        }

        public async Task<IActionResult> ControlePonto(DateTime? dataInicial, DateTime? dataFinal, int? funcionarioId)
        {
            var applicationDbContext = _context.RegistraPontos.Include(r => r.Funcionario);



            var query = _context.RegistraPontos.AsQueryable();

            // Aplica o filtro por data, se informado
            if (dataInicial.HasValue && dataFinal.HasValue)
            {
                // Filtro de intervalo de datas
                query = query.Where(rp => rp.Data.Date >= dataInicial.Value.Date && rp.Data.Date <= dataFinal.Value.Date);
            }
           

            // Aplica o filtro por funcionário, se informado
            if (funcionarioId.HasValue)
            {
                query = query.Where(rp => rp.FuncionarioId == funcionarioId.Value);
            }

            var registrosPonto = await query.ToListAsync();

            // Obter a lista de funcionários registrados nos pontos
            var funcionarios = await _context.Funcionarios.ToListAsync();

            // Passar a lista de funcionários para a view
            ViewData["Funcionarios"] = new SelectList(funcionarios, "Id", "Nome");

            return View(registrosPonto);



        }

        public async Task<IActionResult> Relatorio(DateTime? dataInicial, DateTime? dataFinal, int? funcionarioId)
        {

            var registros = await _context.RegistraPontos
                .Include(f=> f.Funcionario)
                .ToListAsync();
            var query = _context.RegistraPontos.AsQueryable();
            // Aplica o filtro por data, se informado
            if (dataInicial.HasValue && dataFinal.HasValue)
            {
                // Filtro de intervalo de datas
                query = query.Where(rp => rp.Data.Date >= dataInicial.Value.Date && rp.Data.Date <= dataFinal.Value.Date);
            }
            // Aplica o filtro por funcionário, se informado
            if (funcionarioId.HasValue)
            {
                query = query.Where(rp => rp.FuncionarioId == funcionarioId.Value);
            }

            var registrosPonto = await query.ToListAsync();

            // Obter a lista de funcionários registrados nos pontos
            var funcionarios = await _context.Funcionarios.ToListAsync();

            // Passar a lista de funcionários para a view
            ViewData["Funcionarios"] = new SelectList(funcionarios, "Id", "Nome");

            TempData["DataInicial"] = dataInicial;
            TempData["DataFinal"] = dataFinal;
            TempData["FuncionarioId"] = funcionarioId;

            return View(registrosPonto);
        }

        // GET: Relatorio/ExportarPDF
        public async Task<IActionResult> ExportarPDF(DateTime? dataInicial, DateTime? dataFinal, int? funcionarioId)
        {
            
            var query = _context.RegistraPontos.AsQueryable();
            // Aplica o filtro por data, se informado
            if (dataInicial.HasValue && dataFinal.HasValue)
            {
                // Filtro de intervalo de datas
                query = query.Where(rp => rp.Data.Date >= dataInicial.Value.Date && rp.Data.Date <= dataFinal.Value.Date);
            }
            // Aplica o filtro por funcionário, se informado
            if (funcionarioId.HasValue)
            {
                query = query.Where(rp => rp.FuncionarioId == funcionarioId.Value);
            }

            var registrosPonto = await query.ToListAsync();

            // Obter a lista de funcionários registrados nos pontos
            var funcionarios = await _context.Funcionarios.ToListAsync();

            // Passar a lista de funcionários para a view
            
            TempData["FuncionarioId"] = funcionarioId;
            TempData["DataInicial"] = dataInicial;
            TempData["DataFinal"] = dataFinal;
            TempData["FuncionarioId"] = funcionarioId;



            var stream = new MemoryStream();
            var writer = new PdfWriter(stream);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

          

            PageSize pageSize = new PageSize(PageSize.A4.Rotate());
            pdf.SetDefaultPageSize(pageSize);

            var titulo = new Paragraph(" Espelho Ponto" );
            document.Add(titulo);



            // Cabeçalho
            var table = new Table(9).UseAllAvailableWidth();
            table.AddCell(new Cell().Add(new Paragraph(" Colaborador ")));
            table.AddCell(new Cell().Add(new Paragraph(" Sobrenome ")));
            table.AddCell(new Cell().Add(new Paragraph(" Data ")));
            table.AddCell(new Cell().Add(new Paragraph(" 1 Entrada ")));
            table.AddCell(new Cell().Add(new Paragraph(" 1 Saída ")));
            table.AddCell(new Cell().Add(new Paragraph(" Intervalo ")));
            table.AddCell(new Cell().Add(new Paragraph(" 2 Entrada ")));
            table.AddCell(new Cell().Add(new Paragraph(" 2 Saída ")));
            table.AddCell(new Cell().Add(new Paragraph(" Total da Jornada")));

            // Dados
            foreach (var registro in registrosPonto)
            {
                if(registro != null)
                {
                    table.AddCell(new Cell().Add(new Paragraph(registro.Funcionario.Nome)));
                    table.AddCell(new Cell().Add(new Paragraph(registro.Funcionario.Sobrenome)));
                    table.AddCell(new Cell().Add(new Paragraph(registro.Data.ToString("dd/MM/yyyy"))));
                    if (registro.HoraEntrada1 != null)
                    {
                        table.AddCell(new Cell().Add(new Paragraph(registro.HoraEntrada1.Value.ToShortTimeString())));

                    }

                    if (registro.HoraSaida1 != null)
                    {
                        table.AddCell(new Cell().Add(new Paragraph(registro.HoraSaida1.Value.ToShortTimeString())));

                    }

                    if (registro.Intervalo != null)
                    {
                        table.AddCell(new Cell().Add(new Paragraph(registro.Intervalo.Value.ToString("hh\\:mm\\:ss"))));

                    }

                    if (registro.HoraEntrada2!= null)
                    {
                        table.AddCell(new Cell().Add(new Paragraph(registro.HoraEntrada2.Value.ToShortTimeString())));

                    }
                    if (registro.HoraSaida2 != null)
                    {
                        table.AddCell(new Cell().Add(new Paragraph(registro.HoraSaida2.Value.ToShortTimeString())));

                    }
                    if (registro.TotalDeHoras != null)
                    {
                        table.AddCell(new Cell().Add(new Paragraph(registro.TotalDeHoras.Value.ToString("hh\\:mm\\:ss"))));
                        
                    }


                        

                }

               



            }

            document.Add(table);

            // Rodapé do documento
            var dataHoraAtual = DateTime.Now;
            
            var rodape = new Paragraph($"Data: {dataHoraAtual.ToString("dd/MM/yyyy")}   Hora: {dataHoraAtual.ToString("HH:mm:ss")} Ass:_____________________________________________ ");
           

            var numeroPaginas = pdf.GetNumberOfPages();
            document.ShowTextAligned(rodape, 30, 30, numeroPaginas, TextAlignment.LEFT, VerticalAlignment.BOTTOM, 0);
            
            document.Close();

            var content = stream.ToArray();
            return File(content, "application/pdf", "Espelho_de_Ponto.pdf");






        }
    }

}
