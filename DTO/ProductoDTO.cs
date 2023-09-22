using TrabajoPracticoBit.Models;

namespace TrabajoPracticoBit.DTO
{
    public class ProductoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool Activo { get; set; }
        public int Stock { get; set; }
        public decimal? Precio { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string NombreCategoria { get; set; }
        public string NombreMarca { get; set; }
    }
}
