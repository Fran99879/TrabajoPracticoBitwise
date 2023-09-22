using TrabajoPracticoBit.Models;

namespace TrabajoPracticoBit.DAL.Interfaces
{
    public interface IMarcaRepository : IGenericRepository<Marca>
    {
        public Task<IEnumerable<Marca>> ObtenerConProductos();
    }
}
