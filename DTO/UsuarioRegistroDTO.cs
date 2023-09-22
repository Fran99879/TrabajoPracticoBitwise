using System.ComponentModel.DataAnnotations;

namespace TrabajoPracticoBit.DTO
{
    public class UsuarioRegistroDTO
    {
        [Required(ErrorMessage = "Es Necesario un Nombre de Usuario")]
        public string NombreUsuario { get; set; }
        [Required(ErrorMessage = "Es Necesario un Nombre")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Es Necesario una Contraseña")]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
