using System.ComponentModel.DataAnnotations;

namespace TrabajoPracticoBit.DTO
{
    public class UsuarioLoginDTO
    {
        [Required(ErrorMessage = "Es Necesario un Nombre de Usuario")]
        public string NombreUsuario { get; set; }
        [Required(ErrorMessage = "Es Necesario una Contraseña")]
        public string Password { get; set; }
    }
}
