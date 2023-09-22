namespace TrabajoPracticoBit.DAL.Interfaces
{
    public interface IGenericRepository<TEntityModel> where TEntityModel : class
    {
        Task<IEnumerable<TEntityModel>> ObtenerTodos();
        Task<TEntityModel> Obtener(int id);
        Task<bool> Insertar(TEntityModel entity);
        Task<bool> Actualizar(TEntityModel entity);
        Task<bool> Eliminar(int id);
    }
}
