namespace PruebaTec.Models
{
    public class Validaciones
    {
        public bool ValidacionRegistro(string paraValidar, List<string> dondeValidar)
        {
            bool respuesta = dondeValidar.Contains(paraValidar);
            return respuesta;
        }
        public bool ValidacionInicioSesion(string validaCorreo,string validaContrasena, List<string> vCorreos, List<string> vContrasena)
        {
            bool correo = vCorreos.Contains(validaCorreo);
            bool contrasena =  vContrasena.Contains(validaContrasena);
           
            if(correo && contrasena)
            {
                return true;
            }
            return false;
        }

        public string CadenaValidaCorreo(string validaCorreo)
        {
            return "https://localhost:7269/api/ApiValidation/ValidaCorreo?correo="+ validaCorreo ;
        }
    }
}
