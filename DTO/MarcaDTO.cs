using TrabajoPracticoBit.Models;

namespace TrabajoPracticoBit.DTO
{
    public class MarcaDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public HashSet<ProductoDTO> Productos { get; set; } = new HashSet<ProductoDTO>();
    }
}
