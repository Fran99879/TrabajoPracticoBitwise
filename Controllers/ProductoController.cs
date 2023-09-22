using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TrabajoPracticoBit.DAL.Interfaces;
using TrabajoPracticoBit.DTO;
using TrabajoPracticoBit.Models;

namespace TrabajoPracticoBit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IGenericRepository<Producto> _repository;
        private readonly IProductoRepository _productoRepository;
        private readonly IMapper _mapper;


        public ProductoController(IGenericRepository<Producto> repository,
                                  IProductoRepository productoRepository,
                                  IMapper mapper)
        {
            _repository = repository;
            _productoRepository = productoRepository;
            _mapper = mapper;
        }



        
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> ObtenerTodos()
        {
            var producto = await _repository.ObtenerTodos();
            return Ok(producto.ToList());
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> Obtener(int id)
        {

            var producto = await _repository.Obtener(id);
            if (producto == null)
                return NotFound();

            var productoDTO = _mapper.Map<ProductoDTO>(producto);
            return Ok(productoDTO);

        }

        //[ResponseCache(Duration = 15)]   //Cache manual
        //[ResponseCache(CacheProfileName = "PorDefecto")]  //Cache generico
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)] //Para que no me guarde los erros en  la cache
        [HttpGet("DataRelacionada{id}")]
        public async Task<ActionResult<ProductoDTO>>ObtenerDataRelacionada(int id)
        {
            var producto = await _productoRepository.ObtenerRelaciones(id);
            if(producto == null)
                return NotFound();
            var productoDto = _mapper.Map<ProductoDTO>(producto);
            return Ok(productoDto);
        }

        [Authorize(Roles = "user, admin")]
        [HttpPost]
        public async Task<ActionResult> Crear(ProductoCreacionDTO productoCreacionDTO)
        {
            var producto = _mapper.Map<Producto>(productoCreacionDTO);
            await _repository.Insertar(producto);

            var productoDto = _mapper.Map<ProductoDTO>(producto);
            return CreatedAtAction(nameof(Obtener), new { id = producto.Id }, productoDto);

        }
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, ProductoCreacionDTO productoCreacionDTO)
        {
            var producto = await _repository.Obtener(id);
            if (producto == null)
                return NotFound();

            _mapper.Map(productoCreacionDTO, producto);

            var result = await _repository.Actualizar(producto);
            if (result)
                return NoContent();

            return BadRequest();
        }

        [Authorize(Roles = "user, admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var producto = await _repository.Obtener(id);
            if (producto == null)
                return NotFound();
            var result = await _repository.Eliminar(id);
            if (result)
                return NoContent();

            return BadRequest();
        }


    }
}
