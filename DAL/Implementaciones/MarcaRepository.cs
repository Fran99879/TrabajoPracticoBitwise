using Microsoft.EntityFrameworkCore;
using TrabajoPracticoBit.DAL.DataContext;
using TrabajoPracticoBit.DAL.Interfaces;
using TrabajoPracticoBit.Models;

namespace TrabajoPracticoBit.DAL.Implementaciones
{
    public class MarcaRepository : GenericRepository<Marca>, IMarcaRepository
    {
        private readonly ApplicationDbContext _context;
        public MarcaRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Marca>> ObtenerConProductos()
        {
            return await _context.Marcas.Include(p => p.Productos).ToListAsync();
        }
    }
}
