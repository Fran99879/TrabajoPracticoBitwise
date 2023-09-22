namespace TrabajoPracticoBit.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool Activo { get; set; }
        public int Stock { get; set; }
        public decimal? Precio { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; } = null!;
        public int MarcaId { get; set; }
        public Marca Marca { get; set; } = null!;
    }
}
