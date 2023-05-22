using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc;
using sistema_de_ponto.Models;

namespace sistema_de_ponto.Controllers
{
    public class FuncionariosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public FuncionariosController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
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
        public async Task<IActionResult> Create([Bind("Id,Nome,Sobrenome,Cpf,Pis,Departamento,Cargo,Telefone,Email,Senha,Perfil,Foto, Arquivo,EmpresaId")] Funcionario funcionario)
        {
            //Pegando a extensão do arquivo
            string extensao = Path.GetExtension(funcionario.Arquivo.FileName);

            //Garantindo um nome "único" para o arquivo.
            string nomeUnico = Guid.NewGuid().ToString();

            //Pegando a pasta de arquivos estáticos
            string caminho = Path.Combine(_env.ContentRootPath, "Arquivos", nomeUnico + extensao);

            funcionario.Foto = nomeUnico + extensao;

            if (ModelState.IsValid)
            {
                if (funcionario.Arquivo.Length > 0)
                {
                    using (Stream fileStream = new FileStream(caminho, FileMode.Create))
                    {
                        await funcionario.Arquivo.CopyToAsync(fileStream);
                    }
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Sobrenome,Cpf,Pis,Departamento,Cargo,Telefone,Email,Senha,Perfil,Foto,EmpresaId")] Funcionario funcionario)
        {
            if (id != funcionario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
    }
}
