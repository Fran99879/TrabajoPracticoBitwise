namespace TrabajoPracticoBit.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public string  NombreCliente { get; set; }
        //public int Cantidad { get; set; }
        //public decimal Total { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
    }
}
