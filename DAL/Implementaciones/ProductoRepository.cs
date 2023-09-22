using Microsoft.EntityFrameworkCore;
using TrabajoPracticoBit.DAL.DataContext;
using TrabajoPracticoBit.DAL.Interfaces;
using TrabajoPracticoBit.Models;

namespace TrabajoPracticoBit.DAL.Implementaciones
{
    public class ProductoRepository : GenericRepository<Producto>, IProductoRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Producto> ObtenerRelaciones(int id)
        {
            var query = await _context.Productos
                .Include(c => c.Categoria)
                .Include(m => m.Marca)
                .FirstOrDefaultAsync(p => p.Id == id);
            return query;
        }
    }
}
