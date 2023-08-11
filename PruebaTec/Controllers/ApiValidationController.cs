using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PruebaTec.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PruebaTec.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiValidationController : ControllerBase
    {
        private readonly PtitGroupContext _context;
        public ApiValidationController(PtitGroupContext context)
        {
            _context = context;
        }
        [HttpGet("ValidaCorreo")]
        public IActionResult VerificarCorreo(string correo)
        {
            // Aquí tendrías que descifrar el valor de "codigo" y obtener el correo
            // Por simplicidad, asumiré que ya tienes el correo       

            var cliente = _context.Clientes.FirstOrDefault(c => c.Correo == correo);

            if (cliente == null)
            {
                return NotFound("Cliente no encontrado.");
            }

            cliente.ClienteValidacion = true;
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
