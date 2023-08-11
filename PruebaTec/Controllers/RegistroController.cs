using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTec.Models;
using PruebaTec.Models.SendEmail;
using PruebaTec.Models.ViewModels;
using System.Security.Claims;

namespace PruebaTec.Controllers
{
    public class RegistroController : Controller
    {
        private readonly PtitGroupContext _context;
        private HashConverter _hash;
        private Validaciones _validator;
        private readonly IEmailSender _emailSender;
        public RegistroController(PtitGroupContext context, IEmailSender emailSender)
        {
            _context = context;
            _hash = new HashConverter();
            _validator = new Validaciones();
            _emailSender = emailSender;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult IniciarSesion()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IniciarSesion(InicioSesionViewModel model)
        {
            if (ModelState.IsValid)
            {
                List<string> validaCorreo = _context.Clientes.Select(correo => correo.Correo).ToList();
                List<string> validaContrasena = _context.Clientes.Select(contrasena => contrasena.Contraseña).ToList();
                if (_validator.ValidacionInicioSesion(model.correo, _hash.Hashear(model.contrasena), validaCorreo, validaContrasena))
                {
                    var user = await _context.Clientes.FirstOrDefaultAsync(c => c.Correo == model.correo);
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.correo),
new Claim("CampoBool", user.ClienteValidacion.GetValueOrDefault() ? "true" : "false")
            };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["ContrasenaIncorrecta"] = "Correo o Contraseña son invalidos intentelo de nuevo .";
                    return RedirectToAction(nameof(IniciarSesion));
                }
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarUsuario(RegistroViewModel model)
        {
            if (ModelState.IsValid)
            {
                List<string> validacion = _context.Clientes.Select(correo => correo.Correo).ToList();
                if (!_validator.ValidacionRegistro(model.correo, validacion))
                {
                    var usuario = new Cliente()
                    {
                        Usuario = model.nombre,
                        Correo = model.correo,
                        Contraseña = _hash.Hashear(model.contrasena)
                    };
                    _context.Add(usuario);
                    await _context.SaveChangesAsync();
                    string subject = "Validacion de correo";
                    string message = "Hola " + model.nombre + ", bienvenido a mi prueba tecnica para validar correo haga clic la siguiente liga: " + _validator.CadenaValidaCorreo(model.correo);
                    await _emailSender.SendEmailAsync(model.correo, subject, message);
                    var user = await _context.Clientes.FirstOrDefaultAsync(c => c.Correo == model.correo);

                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.correo),
new Claim("CampoBool", user.ClienteValidacion.GetValueOrDefault() ? "true" : "false")
            };


                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["CorreoRegistrado"] = "El correo ya está registrado.";
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
    }
}
