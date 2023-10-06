using Microsoft.AspNetCore.Mvc;
using Controladora;
using DTO;
using Microsoft.AspNetCore.Cors;
using System.Runtime.CompilerServices;

using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace WebApi_OpenBootcamp.Controllers
{
    [EnableCors]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class InicioSesionController : ControllerBase
    {

        CtrUsuario ctr = null;
        private readonly string? secretKey;
        public InicioSesionController(IConfiguration config)
        {
            ctr = new CtrUsuario();
            secretKey = config.GetSection("settings").GetSection("secretkey").ToString();
        }
        [HttpPost]
        //public DtoUsuario InicioSesion(string usuario, string password)
        public DtoUsuario VerificarAcceso(DtoUsuario entidad)
        //public IActionResult VerificarAcceso(DtoUsuario entidad)
        {
            DtoUsuario data = new DtoUsuario();
            //DtoUsuario entidad = new DtoUsuario();
            //entidad.C_DNI = usuario;
            //entidad.C_PASSWORD = password;
            entidad.PASSWORD = Encrypt.Encrypt.GetSHA256(entidad.PASSWORD);
            data = ctr.InicioSesion(entidad);
            //string contraEncriptada = Encrypt.Encrypt.GetSHA256(entidad.C_PASSWORD);

            if (data != null)
            {
                var keyBytes = Encoding.ASCII.GetBytes(secretKey);
                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, entidad.DNI));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes),
                    SecurityAlgorithms.HmacSha256)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
                string tokenCreado = tokenHandler.WriteToken(tokenConfig);

                data.C_TOKEN = tokenCreado;
                //return StatusCode(StatusCodes.Status200OK, new { response = data });

            }
            return data;
            //else
            //    return StatusCode(StatusCodes.Status401Unauthorized, new { response = "" });

        }
    }
}
