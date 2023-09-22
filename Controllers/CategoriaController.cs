using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TrabajoPracticoBit.DAL.Implementaciones;
using TrabajoPracticoBit.DAL.Interfaces;
using TrabajoPracticoBit.DTO;
using TrabajoPracticoBit.Models;

namespace TrabajoPracticoBit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly IGenericRepository<Categoria> _repository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public CategoriaController(IGenericRepository<Categoria> repository,
                                       IMapper mapper,
                                       ICategoriaRepository categoriaRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _categoriaRepository = categoriaRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaCreacionDTO>>> ObtenerTodos2()
        {
            var categoria = await _repository.ObtenerTodos();
            return Ok(categoria.ToList());
        }

        [HttpGet("{id}", Name = "GetCategoria")]
        public async Task<ActionResult<CategoriaCreacionDTO>> Obtener(int id)
        {
            var cat = await _repository.Obtener(id);
            if (cat == null)
                return NotFound();

            var proDto = _mapper.Map<CategoriaDTO>(cat);

            return new CreatedAtRouteResult("GetCategoria", new { id = cat.Id }, proDto);
        }


        [HttpGet("catConProductos")]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> ObtenerConProductos()
        {
            var catConPro = await _categoriaRepository.ObtenerCategoriaConProductos();
            var catConProDTO = _mapper.Map<IEnumerable<CategoriaDTO>>(catConPro);
            return Ok(catConProDTO);
        }


        [HttpPost]
        public async Task<ActionResult> Crear(CategoriaCreacionDTO categoriaCreacionDTO)
        {
            var categoria = _mapper.Map<Categoria>(categoriaCreacionDTO);

            await _repository.Insertar(categoria);

            var generoDto = _mapper.Map<CategoriaDTO>(categoria);

            return Ok(generoDto);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, CategoriaCreacionDTO categoriaCreacionDTO)
        {
            var cat = await _repository.Obtener(id);
            if (cat == null)
                return NotFound();

            _mapper.Map(categoriaCreacionDTO, cat);

            var result = await _repository.Actualizar(cat);
            if (result)
                return NoContent();

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var cat = await _repository.Obtener(id);
            if (cat == null)
                return NotFound();
            var catResult = await _repository.Eliminar(id);
            if (catResult)
                return NoContent();

            return BadRequest();
        }



    }

}
