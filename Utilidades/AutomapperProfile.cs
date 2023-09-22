using AutoMapper;
using System.Runtime.CompilerServices;
using TrabajoPracticoBit.DTO;
using TrabajoPracticoBit.Models;

namespace TrabajoPracticoBit.Utilidades
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Categoria, CategoriaDTO>();
            CreateMap<CategoriaCreacionDTO, Categoria>();
           
            CreateMap<Marca, MarcaDTO>();
            CreateMap<MarcaCreacionDTO, Marca>();

            CreateMap<Pedido, PedidoDTO>();

            CreateMap<Producto, ProductoDTO>()
                .ForMember(d => d.NombreCategoria, o => o.MapFrom(src => src.Categoria.Nombre))
                .ForMember(d => d.NombreMarca, o => o.MapFrom(src => src.Marca.Nombre))
                .ForMember(d => d.FechaCreacion, opt => opt.MapFrom(o => o.FechaCreacion.ToString("dd/MM/yyyy")));

            CreateMap<ProductoCreacionDTO, Producto>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Categoria, o => o.Ignore())
                .ForMember(d => d.Marca, o => o.Ignore());

            CreateMap<Usuario, UsuarioDTO>().ReverseMap();

            CreateMap<Pedido, PedidoDTO>()
                .ForMember(d => d.NombreProducto, o => o.MapFrom(src => src.Producto.Nombre))
                .ForMember(d => d.PrecioProducto, o => o.MapFrom(src => src.Producto.Precio))
               .ForMember(d => d.StockProducto, o => o.MapFrom(src => src.Producto.Stock));
            CreateMap<PedidoCreacionDTO, Pedido>()
                .ForMember(d => d.Id, o => o.Ignore());

        }
    }
}
