using TrabajoPracticoBit.Models;

namespace TrabajoPracticoBit.DAL.Interfaces
{
    public interface ICategoriaRepository : IGenericRepository<Categoria>
    {
        public Task<IEnumerable<Categoria>> ObtenerCategoriaConProductos();
    }
}
