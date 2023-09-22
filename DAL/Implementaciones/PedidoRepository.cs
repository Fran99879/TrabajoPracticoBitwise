using Microsoft.EntityFrameworkCore;
using TrabajoPracticoBit.DAL.DataContext;
using TrabajoPracticoBit.DAL.Interfaces;
using TrabajoPracticoBit.Models;

namespace TrabajoPracticoBit.DAL.Implementaciones
{
    public class PedidoRepository : GenericRepository<Pedido>, IPedidoRepository
    {
        private readonly ApplicationDbContext _context;

        public PedidoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Pedido> ObtenerProducto(int id)
        {

            var query = await _context.Pedidos
                                .Include(p => p.Producto)
                                .FirstOrDefaultAsync(p => p.Id == id);
            return query;
        }
    }
}
