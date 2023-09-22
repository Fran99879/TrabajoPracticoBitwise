using TrabajoPracticoBit.Models;

namespace TrabajoPracticoBit.DAL.Interfaces
{
    public interface IProductoRepository : IGenericRepository<Producto>
    {
        public Task<Producto> ObtenerRelaciones(int id);
    }
}
