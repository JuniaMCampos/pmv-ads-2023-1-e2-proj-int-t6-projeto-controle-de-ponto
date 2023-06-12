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
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc;
using sistema_de_ponto.Models;
using Path = System.IO.Path;


namespace sistema_de_ponto.Controllers
{
    [Authorize]
    public class FuncionariosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public FuncionariosController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([Bind("Email, Senha")] Funcionario funcionario)
        {
            var func = await _context.Funcionarios
                .FirstOrDefaultAsync(m => m.Email == funcionario.Email);

            if (func == null)
            {
                ViewBag.Message = "Usuário e/ou senha inválidos!";
                return View();
            }

            bool isSenhaOk = BCrypt.Net.BCrypt.Verify(funcionario.Senha, func.Senha);

            if (isSenhaOk)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, func.Nome),
                    new Claim(ClaimTypes.NameIdentifier, func.Nome),
                    new Claim(ClaimTypes.Role, func.Perfil.ToString())
                };

                var userIdentity = new ClaimsIdentity(claims, "login");

                ClaimsPrincipal principal = new(userIdentity);

                var props = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTime.Now.ToLocalTime().AddDays(7),
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(principal, props);

                return Redirect("Home/Index");
            }

            ViewBag.Message = "Usuário e/ou senha inválidos!";
            
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Funcionarios");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        // GET: Funcionarios

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Funcionarios.Include(f => f.Empresa);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Funcionarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios
                .Include(f => f.Empresa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // GET: Funcionarios/Create
        public IActionResult Create()
        {
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "Id", "Nome");
            return View();
        }

        // POST: Funcionarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Gestor")]
        public async Task<IActionResult> Create([Bind("Id,Nome,Sobrenome,Cpf,Pis,Departamento,Cargo,Telefone,Email,Senha,Perfil,Foto, Arquivo,EmpresaId")] Funcionario funcionario)
        {
            

            if (ModelState.IsValid)
            {
                if (funcionario.Arquivo !=null && funcionario.Arquivo.Length > 0)
                {
                    //Pegando a extensão do arquivo
                    string extensao = Path.GetExtension(funcionario.Arquivo.FileName);

                    //Garantindo um nome "único" para o arquivo.
                    string nomeUnico = Guid.NewGuid().ToString();

                    //Pegando a pasta de arquivos estáticos
                    string caminho = Path.Combine(_env.ContentRootPath, "Arquivos", nomeUnico + extensao);

                    funcionario.Foto = nomeUnico + extensao;

                    using (Stream fileStream = new FileStream(caminho, FileMode.Create))
                    {
                        await funcionario.Arquivo.CopyToAsync(fileStream);
                    }
                }
                else
                {
                    funcionario.Foto = null;
                }
                
                funcionario.Senha = BCrypt.Net.BCrypt.HashPassword(funcionario.Senha);
                _context.Add(funcionario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "EmpresaId", "EmpresaId");
            return View(funcionario);
        }

        // GET: Funcionarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                
                return NotFound();
            }

            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "Id", "Nome", funcionario.EmpresaId);
            return View(funcionario);
        }

        // POST: Funcionarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Sobrenome,Cpf,Pis,Departamento,Cargo,Telefone,Email,Senha,Perfil,Foto,Arquivo,EmpresaId")] Funcionario funcionario)
        {
            if (id != funcionario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (funcionario.Arquivo != null && funcionario.Arquivo.Length > 0)
                {
                    //Pegando a extensão do arquivo
                    string extensao = Path.GetExtension(funcionario.Arquivo.FileName);

                    //Garantindo um nome "único" para o arquivo.
                    string nomeUnico = Guid.NewGuid().ToString();

                    //Pegando a pasta de arquivos estáticos
                    string caminho = Path.Combine(_env.ContentRootPath, "Arquivos", nomeUnico + extensao);

                    funcionario.Foto = nomeUnico + extensao;

                    using (Stream fileStream = new FileStream(caminho, FileMode.Create))
                    {
                        await funcionario.Arquivo.CopyToAsync(fileStream);
                    }
                }
                else
                {
                    
                        funcionario.Foto = null;
                    
                }

                try
                {
                    funcionario.Senha = BCrypt.Net.BCrypt.HashPassword(funcionario.Senha);
                    _context.Update(funcionario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionarioExists(funcionario.Id))
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
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "EmpresaId", "EmpresaId", funcionario.EmpresaId);
            return View(funcionario);
        }

        // GET: Funcionarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios
                .Include(f => f.Empresa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionarioExists(int id)
        {
            return _context.Funcionarios.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Relatorio()
        {
           
            var funcionarios = await _context.Funcionarios
                .Include(f => f.Empresa)
                .ToListAsync();
           
            return View(funcionarios);
        }

        // GET: Relatorio/ExportarPDF
        public IActionResult ExportarPDF()
        {
            var funcionarios = _context.Funcionarios
                .Include(f => f.Empresa)
                .ToList();

            
            var memoryStream = new MemoryStream();

            var writer = new PdfWriter(memoryStream);
            var pdfDocument = new PdfDocument(writer);
            Document document = new(pdfDocument);

            PageSize pageSize = new PageSize(PageSize.A4.Rotate());
            pdfDocument.SetDefaultPageSize(pageSize);

            var titulo = new Paragraph("Relatório de Colaboradores");
            document.Add(titulo);



            // Cabeçalho
            var table = new Table(11).UseAllAvailableWidth();
            table.AddCell(new Cell().Add(new Paragraph(" ID ")));
            table.AddCell(new Cell().Add(new Paragraph(" Colaborador ")));
            table.AddCell(new Cell().Add(new Paragraph(" Sobrenome ")));
            table.AddCell(new Cell().Add(new Paragraph(" Departamento")));
            table.AddCell(new Cell().Add(new Paragraph(" Empresa")));
            table.AddCell(new Cell().Add(new Paragraph(" Cargo ")));
            table.AddCell(new Cell().Add(new Paragraph(" Cpf")));
            table.AddCell(new Cell().Add(new Paragraph(" Pis ")));
            table.AddCell(new Cell().Add(new Paragraph("Telefone ")));
            table.AddCell(new Cell().Add(new Paragraph(" Email")));
            table.AddCell(new Cell().Add(new Paragraph(" Perfil ")));

            // Dados
            foreach (var item in funcionarios)
            {


                table.AddCell(new Cell().Add(new Paragraph(item.Id.ToString())));
                table.AddCell(new Cell().Add(new Paragraph(item.Nome)));
                table.AddCell(new Cell().Add(new Paragraph(item.Sobrenome)));
                table.AddCell(new Cell().Add(new Paragraph(item.Departamento)));
                table.AddCell(new Cell().Add(new Paragraph(item.Empresa.Nome)));
                table.AddCell(new Cell().Add(new Paragraph(item.Cargo)));
                table.AddCell(new Cell().Add(new Paragraph(item.Cpf)));
                table.AddCell(new Cell().Add(new Paragraph(item.Pis)));
                table.AddCell(new Cell().Add(new Paragraph(item.Telefone)));
                table.AddCell(new Cell().Add(new Paragraph(item.Email)));
                table.AddCell(new Cell().Add(new Paragraph(item.Perfil.ToString())));



            }

            document.Add(table);

            // Rodapé do documento
            var dataHoraAtual = DateTime.Now;
            var rodape = new Paragraph($"Apontei Sistemas Data: {dataHoraAtual.ToString("dd/MM/yyyy")}   Hora: {dataHoraAtual.ToString("HH:mm:ss")}");


            var numeroPaginas = pdfDocument.GetNumberOfPages();
            document.ShowTextAligned(rodape, 30, 30, numeroPaginas, TextAlignment.LEFT, VerticalAlignment.BOTTOM, 0);

            document.Close();

            var content = memoryStream.ToArray();
            return File(content, "application/pdf", "Relatorio_de_Colaboradores.pdf");






        }
    }
}
