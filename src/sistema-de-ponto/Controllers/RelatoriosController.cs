using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace sistema_de_ponto.Controllers
{
    [Authorize]
    public class RelatoriosController : Controller
    {

        public IActionResult Index()
        {

            return View();
        }
    }
}
