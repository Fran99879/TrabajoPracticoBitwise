using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TrabajoPracticoBit.DAL.DataContext;
using TrabajoPracticoBit.DAL.Interfaces;
using TrabajoPracticoBit.DTO;
using TrabajoPracticoBit.Models;

namespace TrabajoPracticoBit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IGenericRepository<Usuario> _repository;
        private readonly IUsuarioRepository _userRepository;
        private readonly IMapper _mapper;
        protected RespuestaApi _respuesta;

        public UsuarioController(IGenericRepository<Usuario> repository, IUsuarioRepository userRepository, IMapper mapper)
        {
            _repository = repository;
            _userRepository = userRepository;
            _mapper = mapper;
            this._respuesta = new();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>>ObtenerTodosLosUsuarios()
        {
            var usuarios = await _repository.ObtenerTodos();
            var usuariosDTO = _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
            return Ok(usuariosDTO);
        }

        [HttpPost("Registro")]
        public async Task<IActionResult> Registro([FromBody] UsuarioRegistroDTO usuarioRegistroDTO)
        {
            var validationName = await _userRepository.IsUniqueUser(usuarioRegistroDTO.NombreUsuario);
            if (!validationName)
            {
                _respuesta.StatusCode = HttpStatusCode.BadRequest;
                _respuesta.IsSucces = false;
                _respuesta.ErrorMenssages.Add("El Nombre del usuario ya existe");
                return BadRequest(_respuesta);
            }

            var user = await _userRepository.Registro(usuarioRegistroDTO);
            if (user == null)
            {
                _respuesta.StatusCode = HttpStatusCode.BadRequest;
                _respuesta.IsSucces = false;
                _respuesta.ErrorMenssages.Add("Error al registrarse");
                return BadRequest(_respuesta);
            }

            _respuesta.StatusCode = HttpStatusCode.OK;
            _respuesta.IsSucces = true;
            return BadRequest(_respuesta);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDTO UsuarioLoginDTO)
        {
            var respuestaLogin = await _userRepository.Login(UsuarioLoginDTO);
            if (respuestaLogin.Usuario == null || string.IsNullOrEmpty(respuestaLogin.Token))
            {
                _respuesta.StatusCode = HttpStatusCode.BadRequest;
                _respuesta.IsSucces = false;
                _respuesta.ErrorMenssages.Add("EL Usuario o el Password Son Incorrectos");
                return BadRequest(_respuesta);
            }
            _respuesta.StatusCode = HttpStatusCode.BadRequest;
            _respuesta.IsSucces = true;
            _respuesta.Result = respuestaLogin;
            return BadRequest(_respuesta);
        }


    }
}
