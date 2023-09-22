using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TrabajoPracticoBit.DAL.DataContext;
using TrabajoPracticoBit.DAL.Interfaces;
using TrabajoPracticoBit.DTO;
using TrabajoPracticoBit.Models;
using XSystem.Security.Cryptography;

namespace TrabajoPracticoBit.DAL.Implementaciones
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;
        private string claveSecreta;
        private readonly IMapper _mapper;
        public UsuarioRepository(ApplicationDbContext context, 
                                 IConfiguration config,
                                 IMapper mapper) : base(context)
        {
            _context = context;
            claveSecreta = config.GetValue<string>("ApiSettings:Secreta");
            _mapper = mapper;
        }

        public async Task<bool> IsUniqueUser(string usuario)
        {
            var usuarioDb = await _context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == usuario);
            if (usuarioDb == null)
            {
                return true;
            }
            return false;
        }

        public async Task<UsuarioLoginRespuestaDTO> Login(UsuarioLoginDTO usuarioLoginDTO)
        {
            var passwordEncrip = ObtenerMD5(usuarioLoginDTO.Password);
            var usuarioEncontrado = await _context.Usuarios.FirstOrDefaultAsync(
                                                u => u.NombreUsuario.ToLower() == usuarioLoginDTO.NombreUsuario.ToLower()
                                               && u.Password == passwordEncrip);


            if (usuarioEncontrado == null)
            {
                return new UsuarioLoginRespuestaDTO()
                {
                    Token = "",
                    Usuario = null
                };
            }

            var manejadorToken = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(claveSecreta);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuarioEncontrado.NombreUsuario.ToString()),
                    new Claim(ClaimTypes.Role, usuarioEncontrado.Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = manejadorToken.CreateToken(tokenDescriptor);

            UsuarioLoginRespuestaDTO usuarioLoginRespuestaDTO = new UsuarioLoginRespuestaDTO()
            {
                Token = manejadorToken.WriteToken(token),
                Usuario = _mapper.Map<Usuario>(usuarioEncontrado)
            };
            return usuarioLoginRespuestaDTO;
        }

        public async Task<Usuario> Registro(UsuarioRegistroDTO usuarioRegistroDTO)
        {
            var passwordEncrip = ObtenerMD5(usuarioRegistroDTO.Password);

            var usuarioN = new Usuario()
            {
                NombreUsuario = usuarioRegistroDTO.NombreUsuario,
                Password = passwordEncrip,
                Nombre = usuarioRegistroDTO.Nombre,
                Role = usuarioRegistroDTO.Role
            };

            _context.Usuarios.Add(usuarioN);
            await _context.SaveChangesAsync();
            usuarioN.Password = passwordEncrip;
            return usuarioN;
        }

        public static string ObtenerMD5(string valor)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] data = Encoding.UTF8.GetBytes(valor);
            data = x.ComputeHash(data);
            string respuesta = "";
            for (int i = 0; i < data.Length; i++)
            {
                respuesta += data[i].ToString("x2").ToLower();
            }
            return respuesta;
        }

    }
}
