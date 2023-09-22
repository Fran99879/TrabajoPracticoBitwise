using Microsoft.EntityFrameworkCore;
using TrabajoPracticoBit.DAL.DataContext;
using TrabajoPracticoBit.DAL.Interfaces;

namespace TrabajoPracticoBit.DAL.Implementaciones
{
    public class GenericRepository<TEntityModel> : IGenericRepository<TEntityModel> where TEntityModel : class
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<bool> Actualizar(TEntityModel entity)
        {          
            bool resultado = false;
            
            _context.Set<TEntityModel>().Update(entity);
            resultado = await _context.SaveChangesAsync() > 0;
            return resultado;
        }

        public async Task<bool> Eliminar(int id)
        {
            var entidad = await Obtener(id);
            if (entidad == null)
            {
                return false;

            }
            _context.Set<TEntityModel>().Remove(entidad);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Insertar(TEntityModel entity)
        {
            bool resultado = false;
            
            _context.Set<TEntityModel>().AddAsync(entity);
            resultado = await _context.SaveChangesAsync() > 0;
            return resultado;

        }

        public async Task<TEntityModel> Obtener(int id)
        {
            return await _context.Set<TEntityModel>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntityModel>> ObtenerTodos()
        {
            return await _context.Set<TEntityModel>().ToListAsync();
        }
    }
}
