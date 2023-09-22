namespace TrabajoPracticoBit.Models
{
    public class Marca
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public HashSet<Producto> Productos { get; set; } = new HashSet<Producto>();
    }
}
