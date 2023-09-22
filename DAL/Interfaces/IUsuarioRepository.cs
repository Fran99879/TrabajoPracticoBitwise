using TrabajoPracticoBit.DTO;
using TrabajoPracticoBit.Models;

namespace TrabajoPracticoBit.DAL.Interfaces
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        Task<bool> IsUniqueUser(string usuario);
        Task<UsuarioLoginRespuestaDTO> Login(UsuarioLoginDTO usuarioLoginDTO);
        Task<Usuario> Registro(UsuarioRegistroDTO usuarioRegistroDTO);

    }
}
