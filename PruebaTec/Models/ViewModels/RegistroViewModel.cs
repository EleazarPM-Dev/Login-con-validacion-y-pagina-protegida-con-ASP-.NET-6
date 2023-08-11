using System.ComponentModel.DataAnnotations;

namespace PruebaTec.Models.ViewModels
{
    public class RegistroViewModel
    {
        [Required] 
        public string nombre { get; set; }
        [Required]
        [EmailAddress]
        public string correo { get; set; }
        [Required]
        public string contrasena { get; set; }

    }
}
