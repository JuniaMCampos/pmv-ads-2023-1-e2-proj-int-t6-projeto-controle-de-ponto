using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sistema_de_ponto.Models;

namespace sistema_de_ponto.Controllers
{
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
            var applicationDbContext = _context.RegistraPontos.Include(r => r.Funcionario);
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
                  
           
                var novoRegistroPonto = new RegistraPonto
                {
                    Data = DateTime.Today,
                    FuncionarioId = (int)funcionarioId,

                };
               

                if (ultimoRegistroPonto == null  || ultimoRegistroPonto.HoraSaida2 != null)
                {
                _context.Add(novoRegistroPonto);
                // Se não houver registros anteriores, define a hora de entrada 1
                novoRegistroPonto.HoraEntrada1 = DateTime.Now;
                }
                else if (ultimoRegistroPonto.HoraSaida1 == null)
                {
                    // Se a hora de saída 1 não estiver definida, define a hora de saída 1
                    ultimoRegistroPonto.HoraSaida1 = DateTime.Now;
                }
                else if (ultimoRegistroPonto.HoraEntrada2 == null)
                {
                    // Se a hora de entrada 2 não estiver definida, define a hora de entrada 2
                    ultimoRegistroPonto.HoraEntrada2 = DateTime.Now;
                }
                else if (ultimoRegistroPonto.HoraSaida2 == null)
                {
                    // Se a hora de saída 2 não estiver definida, define a hora de saída 2
                    ultimoRegistroPonto.HoraSaida2 = DateTime.Now;
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
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Cargo", registraPonto.FuncionarioId);
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Cargo", registraPonto.FuncionarioId);
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
            return RedirectToAction(nameof(Index));
        }

        private bool RegistraPontoExists(int id)
        {
            return _context.RegistraPontos.Any(e => e.Id == id);
        }

        public async Task<IActionResult> ControlePonto(DateTime? data, int? funcionarioId)
        {
            var applicationDbContext = _context.RegistraPontos.Include(r => r.Funcionario);



            var query = _context.RegistraPontos.AsQueryable();

            // Aplica o filtro por data, se informado
            if (data.HasValue)
            {
                query = query.Where(rp => rp.Data.Date == data.Value.Date);
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
    }

}
