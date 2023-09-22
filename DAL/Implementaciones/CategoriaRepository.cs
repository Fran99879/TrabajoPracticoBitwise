using Microsoft.EntityFrameworkCore;
using TrabajoPracticoBit.DAL.DataContext;
using TrabajoPracticoBit.DAL.Interfaces;
using TrabajoPracticoBit.Models;

namespace TrabajoPracticoBit.DAL.Implementaciones
{
    public class CategoriaRepository : GenericRepository<Categoria>, ICategoriaRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoriaRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Categoria>> ObtenerCategoriaConProductos()
        {
            return await _context.Categorias.Include(p => p.Productos).ToListAsync();
        }
    }
}
