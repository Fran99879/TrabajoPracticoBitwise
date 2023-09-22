using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrabajoPracticoBit.DAL.Implementaciones;
using TrabajoPracticoBit.DAL.Interfaces;
using TrabajoPracticoBit.DTO;
using TrabajoPracticoBit.Models;

namespace TrabajoPracticoBit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {
        private readonly IGenericRepository<Marca> _repository;
        private readonly IMarcaRepository _marcaRepository;
        private readonly IMapper _mapper;

        public MarcaController(IGenericRepository<Marca> repository,
                                  IMapper mapper,
                                  IMarcaRepository marcaRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _marcaRepository = marcaRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarcaCreacionDTO>>> ObtenerTodos2()
        {
            var categoria = await _repository.ObtenerTodos();
            return Ok(categoria.ToList());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<MarcaCreacionDTO>>> Obtener(int id)
        {

            var marcas = await _repository.Obtener(id);
            if (marcas == null)
                return NotFound();

            var marcasDTO = _mapper.Map<MarcaDTO>(marcas);
            return Ok(marcasDTO);

        }


        [HttpGet("marcaConProductos")]
        public async Task<ActionResult<IEnumerable<MarcaDTO>>> ObtenerConProductos()
        {
            var marcasConPro = await _marcaRepository.ObtenerConProductos();
            var marcasConProDTO = _mapper.Map<IEnumerable<MarcaDTO>>(marcasConPro);
            return Ok(marcasConProDTO);
        }


        [HttpPost]
        public async Task<ActionResult> Crear(MarcaCreacionDTO marcaCreacionDTO)
        {
            var marca = _mapper.Map<Marca>(marcaCreacionDTO);

            await _repository.Insertar(marca);

            var marcaDTO = _mapper.Map<MarcaDTO>(marca);

            return Ok(marcaDTO);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, MarcaCreacionDTO marcaCreacionDTO)
        {
            var marca = await _repository.Obtener(id);
            if (marca == null)
                return NotFound();

            _mapper.Map(marcaCreacionDTO, marca);

            var result = await _repository.Actualizar(marca);
            if (result)
                return NoContent();

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var marca = await _repository.Obtener(id);
            if (marca == null)
                return NotFound();
            var marcaResult = await _repository.Eliminar(id);
            if (marcaResult)
                return NoContent();

            return BadRequest();
        }



    }
}
