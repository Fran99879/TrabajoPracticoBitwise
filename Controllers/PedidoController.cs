using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TrabajoPracticoBit.DAL.Implementaciones;
using TrabajoPracticoBit.DAL.Interfaces;
using TrabajoPracticoBit.DTO;
using TrabajoPracticoBit.Models;

namespace TrabajoPracticoBit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IGenericRepository<Pedido> _repository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMapper _mapper;

        public PedidoController(IGenericRepository<Pedido> repository,
                                IPedidoRepository pedidoRepository,
                                  IMapper mapper)
        {
            _repository = repository;
            _pedidoRepository = pedidoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoCreacionDTO>>> ObtenerTodos2()
        {
            var categoria = await _repository.ObtenerTodos();
            return Ok(categoria.ToList());
        }


        [HttpGet("DataRelacionada{id}")]
        public async Task<ActionResult<PedidoDTO>> ObtenerDataRelacionada(int id)
        {
            var pedido = await _pedidoRepository.ObtenerProducto(id);
            if (pedido == null)
                return NotFound();
            var pedidoDto = _mapper.Map<PedidoDTO>(pedido);
            return Ok(pedidoDto);
        }

        
        [HttpPost]
        public async Task<ActionResult> Crear(PedidoCreacionDTO pedidoCreacionDTO)
        {
            var pedido = _mapper.Map<Pedido>(pedidoCreacionDTO);

            await _repository.Insertar(pedido);

            var pedidoDTO = _mapper.Map<PedidoDTO>(pedido);

            return Ok(pedidoDTO);

        }


        [Authorize(Roles = "user")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, PedidoCreacionDTO pedidoCreacionDTO)
        {
            var pedido = await _repository.Obtener(id);
            if (pedido == null)
                return NotFound();

            _mapper.Map(pedidoCreacionDTO, pedido);

            var result = await _repository.Actualizar(pedido);
            if (result)
                return NoContent();

            return BadRequest();
        }

        [Authorize(Roles = "user, admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var pedido = await _repository.Obtener(id);
            if (pedido == null)
                return NotFound();
            var result = await _repository.Eliminar(id);
            if (result)
                return NoContent();

            return BadRequest();
        }



    }
}
