using TrabajoPracticoBit.Models;

namespace TrabajoPracticoBit.DTO
{
    public class ProductoCreacionDTO
    {
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool Activo { get; set; }
        public int Stock { get; set; }
        public decimal? Precio { get; set; }
        public string FechaCreacion { get; set; }
        public int CategoriaId { get; set; }
        public int MarcaId { get; set; }

    }
}
