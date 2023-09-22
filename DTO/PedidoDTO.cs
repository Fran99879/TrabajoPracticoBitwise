using TrabajoPracticoBit.Models;

namespace TrabajoPracticoBit.DTO
{
    public class PedidoDTO
    {
        public int Id { get; set; }
        public string NombreCliente { get; set; }
        //public int Cantidad { get; set; }
        //public decimal Total { get; set; }
        public string NombreProducto { get; set; }
        public string PrecioProducto { get; set; }
        public string StockProducto { get; set; }

    }
}
