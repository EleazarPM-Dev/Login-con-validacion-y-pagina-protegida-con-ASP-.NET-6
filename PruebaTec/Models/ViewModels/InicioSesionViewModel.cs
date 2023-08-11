using System.ComponentModel.DataAnnotations;

namespace PruebaTec.Models.ViewModels
{
    public class InicioSesionViewModel
    {
        [Required]
        [EmailAddress]
        public string correo { get; set; }
        [Required]
        public string contrasena { get; set; }

    }
}
