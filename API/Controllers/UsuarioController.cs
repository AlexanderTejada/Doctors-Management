using Data;
using Data.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using Models.Entidades;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    // *******************************************************************
    // ********************* 🎯 USUARIO CONTROLLER 🎯 *********************
    // *******************************************************************
    public class UsuarioController : BaseApiController
    {
        //private readonly ApplicationDbContext _db; // se deja de trabajar con el DbContext para trabajar con el UserManager
        private readonly UserManager<UsuarioAplicacion> _userManager;
        private readonly ITokenServicio _tokenServicio;
        // ***************************************************************
        // ********************** 🔧 CONSTRUCTOR 🔧 **********************
        // ***************************************************************
        public UsuarioController(UserManager<UsuarioAplicacion> userManager, ITokenServicio tokenServicio)
        {
            _userManager = userManager;
            _tokenServicio = tokenServicio;
        }

        // ***************************************************************
        // ********************** 📬 API ENDPOINTS 📬 *********************
        // ***************************************************************

        // 🌟 ================================================= 🌟
        // 🌟               GET ALL USERS (GET)                🌟
        // 🌟             Endpoint: GET api/usuario            🌟
        // 🌟 ================================================= 🌟
        //[Authorize]
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        //{
        //    var usuarios = await _db.Usuarios.ToListAsync();
        //    return Ok(usuarios);
        //}

        // 🌟 ================================================= 🌟
        // 🌟            GET USER BY ID (GET)                 🌟
        // 🌟         Endpoint: GET api/usuario/{id}          🌟
        // 🌟 ================================================= 🌟
        //[Authorize]
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Usuario>> GetUsuario(int id)
        //{
        //    var usuario = await _db.Usuarios.FindAsync(id);
        //    return Ok(usuario);
        //}

        //// 🌟 ================================================= 🌟
        //// 🌟          REGISTER NEW USER (POST)                 🌟
        //// 🌟        Endpoint: POST api/usuario/registro        🌟
        //// 🌟 ================================================= 🌟
        [HttpPost("registro")]
        public async Task<ActionResult<UsuarioDto>> Registro(RegistroDto registroDto)
        {
            if (await UsuarioExiste(registroDto.UserName)) return BadRequest("El usuario ya existe");

            //  using var hmac = new HMACSHA512();  //creacion de password, ya no es necesario usar porque lo hace identity
            var usuario = new UsuarioAplicacion
            {
                UserName = registroDto.UserName.ToLower(),
               // PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registroDto.Password)),
               // PasswordSalt = hmac.Key
            };

            var resultado = await _userManager.CreateAsync(usuario, registroDto.Password);
            if (!resultado.Succeeded) return BadRequest(resultado.Errors);

            // _db.Usuarios.Add(usuario);
            //await _db.SaveChangesAsync();
            return new UsuarioDto
            {
                Username = usuario.UserName,
                Token =_tokenServicio.CrearToken(usuario)
            };
        }

        // 🌟 ================================================= 🌟
        // 🌟              USER LOGIN (POST)                    🌟
        // 🌟         Endpoint: POST api/usuario/login          🌟
        // 🌟 ================================================= 🌟
        [HttpPost("login")]
        public async Task<ActionResult<UsuarioDto>> Login(LoginDto loginDto)
        {
            var usuario = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.UserName);
            if (usuario == null) return Unauthorized("Usuario incorrecto");

            var resultado = await _userManager.CheckPasswordAsync(usuario, loginDto.Password);
            if (!resultado) return Unauthorized("Contraseña incorrecta");

            //  using var hmac = new HMACSHA512(usuario.PasswordSalt);
            //var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            /* for (int i = 0; i < computedHash.Length; i++)
             {
                 if (computedHash[i] != usuario.PasswordHash[i]) return Unauthorized("Contraseña incorrecta");
             }
            */
            return new UsuarioDto
            {
                Username = usuario.UserName,
                Token = _tokenServicio.CrearToken(usuario)
            };
        }

        // ***************************************************************
        // ******************** 🔒 PRIVATE METHODS 🔒 ********************
        // ***************************************************************

        // 🌟 ================================================= 🌟
        // 🌟          CHECK IF USER EXISTS (PRIVATE)           🌟
        // 🌟 ================================================= 🌟
        private async Task<bool> UsuarioExiste(string UserName)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == UserName.ToLower());
        }
    }
}
