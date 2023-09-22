using TrabajoPracticoBit.Models;

namespace TrabajoPracticoBit.DAL.Interfaces
{
    public interface IPedidoRepository : IGenericRepository<Pedido>
    {
        public Task<Pedido> ObtenerProducto(int id);
    }
}
