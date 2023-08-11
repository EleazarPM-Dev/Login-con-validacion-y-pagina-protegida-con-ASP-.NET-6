using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaTec.Models;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace PruebaTec.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PtitGroupContext _context;
        public HomeController(ILogger<HomeController> logger, PtitGroupContext ptitGroupContext)
        {
            _logger = logger;
            _context = ptitGroupContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy = "CampoBoolPolicy")]
        public async Task <IActionResult> Privacy()
        {
            var usuarioAutenticado = HttpContext.User;
            var claimNombre = usuarioAutenticado.FindFirst(ClaimTypes.Name);
            if (claimNombre != null)
            {

                string nombreUsuario = claimNombre.Value;
                var user = await _context.Clientes.FirstOrDefaultAsync(c => c.Correo == nombreUsuario);
                return View(user);
            }
            return View();
        }

        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
            return RedirectToAction("Index","Registro");

        }

        [AllowAnonymous] 
        public IActionResult AccesoDenegado()
        {
            TempData["Message"] = "No has verificado tu correo electrónico. Por favor, verifica tu correo para acceder.";
            return RedirectToAction("Index", "Home");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}